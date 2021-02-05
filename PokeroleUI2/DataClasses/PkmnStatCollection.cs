using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PokeroleUI2
{
    public class PkmnStatCollection
    {
        public int SpentPoints;
        public int AvailablePoints { get { return MaxPoints - SpentPoints; } }
        public int MaxPoints;

        [XmlIgnoreAttribute]
        private Dictionary<string, PkmnStat> _stats;
        [XmlIgnoreAttribute]
        public Dictionary<string, PkmnStat> Stats {
            get {
                if (_stats == null) {
                    _stats = FromList(StatList);
                }
                return _stats;
            }
            set
            {
                _stats = value;
            }
        }

        public List<PkmnStat> StatList;

        public bool CanSpendPoints { get { return SpentPoints < MaxPoints; } }

        public PkmnStatCollection() { }

        public PkmnStatCollection(List<PkmnStat> stats, int maxpoints, int spentpoints)
        {
            StatList = stats;
            MaxPoints = maxpoints;
            SpentPoints = spentpoints;
            Stats = FromList(stats);
        }

        public Dictionary<string, PkmnStat> FromList(List<PkmnStat> stats)
        {
            Dictionary<string, PkmnStat> statdict = new Dictionary<string, PkmnStat>();
            foreach (PkmnStat s in stats)
            {
                statdict.Add(s.tag, s);
            }
            return statdict;
        }

        public PkmnStatCollection DeepCopy()
        {
            PkmnStatCollection other = (PkmnStatCollection)this.MemberwiseClone();
            Dictionary<string, PkmnStat> stats = new Dictionary<string, PkmnStat>();
            foreach (KeyValuePair<string, PkmnStat> kvp in Stats)
            {
                stats.Add(kvp.Key, kvp.Value.ShallowCopy());
            }
            List<PkmnStat> statlist = new List<PkmnStat>();
            foreach(PkmnStat stat in other.StatList)
            {
                statlist.Add(stat.ShallowCopy());
            }
            other.Stats = stats;
            return other;
        }

        public void SetAllMax(int i) {
            foreach (KeyValuePair<string, PkmnStat> kvp in Stats)
            {
                kvp.Value.SetMax(i);
            }
        }

        public int GetSpentPoints() //this should just be used if we need to do anything weird with loading spent points
        {
            int spentpoints = 0;
            foreach (KeyValuePair<string, PkmnStat> kvp in Stats)
            {
                spentpoints += kvp.Value.AddVal;
            }
            return spentpoints;
        }

        public PkmnStat GetStatByTag(string tag)
        {
            if(tag == null)
            {
                return new PkmnStat("NULLSTAT", 0, 0);
            }

            PkmnStat stat;
            Stats.TryGetValue(tag, out stat);

            if(stat != null) { return stat; }

            return new PkmnStat("NULLSTAT", 0, 0);
        }

        public void AdjustStat(string tag, int p)
        {
            PkmnStat stat = GetStatByTag(tag);
            AdjustStat(stat, p);
        }

        public void AdjustStat(PkmnStat stat, int p)
        {
            if (SpentPoints + p >= MaxPoints + 1)
            {
                return;
            }
            
            if (!Stats.ContainsValue(stat)) { return; }
            SpentPoints += stat.Adjust(p); //adjust will return the actual value so we'll not spend points if something went wrong
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class PkmnStatCollection
    {
        public int SpentPoints;
        public int AvailablePoints { get { return MaxPoints - SpentPoints; } }
        public int MaxPoints;

        public bool CanSpendPoints { get { return SpentPoints < MaxPoints; } }
        public List<PkmnStat> Stats;

        public PkmnStatCollection(List<PkmnStat> stats, int maxpoints, int spentpoints)
        {
            Stats = stats;
        }

        public PkmnStatCollection DeepCopy()
        {
            PkmnStatCollection other = (PkmnStatCollection)this.MemberwiseClone();
            List<PkmnStat> stats = new List<PkmnStat>();
            foreach (PkmnStat s in Stats)
            {
                stats.Add(s.ShallowCopy());
            }
            other.Stats = stats;
            return other;
        }

        public void SetAllMax(int i) {
            foreach(PkmnStat s in Stats)
            {
                s.SetMax(i);
            }
        }

        public int GetSpentPoints() //this should just be used if we need to do anything weird with loading spent points
        {
            int spentpoints = 0;
            foreach (PkmnStat stat in Stats)
            {
                spentpoints += stat.AddVal;
            }
            return spentpoints;
        }

        public PkmnStat GetStatByTag(string tag)
        {
            if(tag == null)
            {
                return new PkmnStat("NULLSTAT", 0, 0);
            }

            foreach (PkmnStat stat in Stats)
            {
                if (stat.tag.ToLower() == tag.ToLower())
                {
                    return stat;
                }
            }
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
            if (!Stats.Contains(stat)) { return; }
            SpentPoints += stat.Adjust(p); //adjust will return the actual value so we'll not spend points if something went wrong
        }
    }
}

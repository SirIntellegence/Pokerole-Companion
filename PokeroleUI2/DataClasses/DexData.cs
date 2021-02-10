using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PokeroleUI2
{
    public class DexData
    {
        public ListData listData;
        public int ID { get; set; }
        public string DexID { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string Rank { get; set; }
        public bool Starter { get; set; }
        public int BaseHP { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public List<string> Abilities { get; set; }
        public List<string> Learnset { get; set; }
        public string AbilitiesString { get
            {
                if (Abilities == null) { return ""; }
                return string.Join(",", Abilities.ToArray());
            }
            set {
                string[] s = value.Split(',');
                Abilities = s.ToList();
            }
        }
        public string LearnsetString
        {
            get
            {
                if(Learnset == null) { return ""; }
                return string.Join(",", Learnset.ToArray());
            }
            set
            {
                string[] s = value.Split(',');
                Learnset = s.ToList();
            }
        }
        public string AttributeString {
            get
            {
                List<string> stats = new List<string>();
                foreach(PkmnStat stat in Attributes.StatList)
                {
                    stats.Add(stat.tag + "," + stat.baseVal + "," + stat.maxVal);
                }
                return String.Join(",", stats);
            }
            set
            {
                
                string[] statsSplit = value.Split(',');
                List<PkmnStat> stats = new List<PkmnStat>();
                for (int i = 0, j = 0; i < statsSplit.Length / 3; i++, j+=3)
                {
                    try { 
                    string sname = statsSplit[j];
                    int val = int.Parse(statsSplit[j + 1]);
                    int max = int.Parse(statsSplit[j + 2]);
                    stats.Add(new PkmnStat(sname, val, max));
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Format Exception reading attributes of pkmn:" + Name);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("IndexOutOfRangeException reading attributes of pkmn:" + Name);
                    }
                }
                Attributes = new PkmnStatCollection(stats, 0, 0);
            }
        }

        public string ImagePath { get; set; }
        public string ThumbPath { get; set; }
        public bool Exclude { get; set; }

        public Brush typebrush1 { get { return PokemonUtils.GetTypeColour(Type1); } }
        public Brush typebrush2 { get { if (string.IsNullOrEmpty(Type2)) { PokemonUtils.GetTypeColour(Type1); } return PokemonUtils.GetTypeColour(Type2); } }

        public BitmapImage RankImg { get { return PokemonUtils.GetRankImage(Rank); } }


        public PkmnStatCollection Attributes;
        public string Description { get; set; }
        
        public DexData()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
        public Brush typebrush2 { get { return PokemonUtils.GetTypeColour(Type2); } }

        public PkmnStatCollection Attributes;
        public string Description { get; set; }
        
        public DexData()
        {

        }

        /*
        public DexData(ListData ld)
        {
            listData = ld;
            ID = listData.ID;
            DexID = listData.DexID;
            Name = listData.Name;
            Type1 = listData.Type1;
            Type2 = listData.Type2;
            Rank = listData.Rank;
            Starter = listData.Starter;
            PopulateData();
        }

        public DexData(int ID)
        {
            PopulateData();
        }

        private void PopulateData()
        {
            using (var db = new PokedexDBEntities())
            {
                var data = (from d in db.DexCores
                            where d.Id == ID
                            select d).SingleOrDefault();

                if (data == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "data");
                }

                DexID = data.DEXID;
                Name = data.NAME;
                Type1 = data.TYPE1;
                Type2 = data.TYPE2;
                Rank = data.RANK;

                var attributeData = (from d in db.AttributesCores
                            where d.Id == ID
                            select d).SingleOrDefault();

                if (attributeData == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "AttributesCore at ID#" + ID);
                }

                PkmnStat STR = new PkmnStat("Strength", (int)attributeData.STR, (int)attributeData.STRMAX);
                PkmnStat DEX = new PkmnStat("Dexterity", (int)attributeData.DEX, (int)attributeData.DEXMAX);
                PkmnStat VIT = new PkmnStat("Vitality", (int)attributeData.VIT, (int)attributeData.VITMAX);
                PkmnStat SPE = new PkmnStat("Special", (int)attributeData.SPE, (int)attributeData.SPEMAX);
                PkmnStat INS = new PkmnStat("Insight", (int)attributeData.INS, (int)attributeData.INSMAX);
                List<PkmnStat> attributesList = new List<PkmnStat> { STR, DEX, VIT, SPE, INS };
                Attributes = new PkmnStatCollection(attributesList, 0, 0);
                BaseHP = (int)attributeData.HP;
                Weight = (int)attributeData.WEIGHT;
                Height = (int)attributeData.HEIGHT;

                var AbilityData = (from d in db.AbilitiesCores
                            where d.Id == ID
                            select d).SingleOrDefault();

                if (AbilityData == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "AbilitiesCore at ID#" + ID);
                }

                string a1 = AbilityData.ABILITY1;
                string a2 = AbilityData.ABILITY2;
                string a3 = AbilityData.ABILITY3;
                string a4 = AbilityData.ABILITY4;

                Abilities = new List<string> { a1, a2, a3, a4 };
                Abilities.RemoveAll(item => item == null);

                var imageData = (from d in db.ImagePaths
                                 where d.DexID == DexID
                                 select d).SingleOrDefault();

                if (imageData == null)
                {
                    ImagePath = "001_Bulbasaur_Dream.png";
                }
                else
                {
                    string[] filenames = imageData.FullPaths.Split(',');
                    if (!String.IsNullOrEmpty(filenames[0])) { ImagePath = filenames[0]; }
                }

                Learnset = new List<string>();

                var learnsetData = (from d in db.LearnsetsCores
                                    where d.Id == ID
                                    select d).SingleOrDefault();

                if (learnsetData == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "LearnsetsCore at ID#" + DexID);
                }

                string[] movenames = learnsetData.Moves.Split(',');
                string[] rankstrings = learnsetData.Ranks.Split(',');

                for (int i = 0; i < movenames.Length; i++)
                {
                    Learnset.Add(movenames[i] + "," + rankstrings[i]);
                }
            }
        }
        */

    }
}

using PokeroleUI2.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace PokeroleUI2
{
    public class MoveData
    {
        [XmlIgnoreAttribute]
        public int ID;
        public string Name { get; set; }
        [XmlIgnoreAttribute]
        public string Type { get; set; }
        [XmlIgnoreAttribute]
        public Brush typebrush { get { return PokemonUtils.GetTypeColour(Type); } }
        [XmlIgnoreAttribute]
        public string Category { get; set; }
        [XmlIgnoreAttribute]
        public BitmapImage CatImg { get { return PokemonUtils.GetCategoryImage(Category); } }

        public string Power { get; set; }
        [XmlIgnoreAttribute]
        public string PowerStat { get; set; }
        [XmlIgnoreAttribute]
        public string AccuracyStat1 { get; set; }
        [XmlIgnoreAttribute]
        public string AccuracyStat2 { get; set; }
        [XmlIgnoreAttribute]
        public string Target { get; set; }
        [XmlIgnoreAttribute]
        public string Effect { get; set; }
        [XmlIgnoreAttribute]
        public string Description { get; set; }
        [XmlIgnoreAttribute]
        public int Rank { get; set; }
        [XmlIgnoreAttribute]
        public string RankString { get; set; }

        [XmlIgnoreAttribute]
        public string AccuracyString { get { return String.IsNullOrEmpty(AccuracyStat2) ? AccuracyStat1 : AccuracyStat1 + " + " + AccuracyStat2; } }
            
        [XmlIgnoreAttribute]
        public string DamageString
        {
            get { return String.IsNullOrEmpty(PowerStat) ? " - " : PowerStat + " + " + Power; }
        }



        public MoveData() {

        }

        public MoveData(string name, int rank = 0)
        {
            Name = name;
            Rank = rank;
            RankString = PokemonUtils.RankFromInt(rank);
            PopulateData();
        }

        private void PopulateData()
        {
            using (var db = new PokedexDBEntities())
            {
                var attributeData = (from d in db.MovesCores
                                     where d.NAME == Name
                                     select d).SingleOrDefault();

                ID = (int)attributeData.Id;
                Type = attributeData.TYPE;
                Category = attributeData.CATEGORY;
                Power = attributeData.POWER;
                PowerStat = attributeData.POWERSTAT;
                AccuracyStat1 = attributeData.ACCURACYSTAT1;
                AccuracyStat2 = attributeData.ACCURACYSTAT2;
                Target = attributeData.TARGET;
                Effect = attributeData.EFFECT;
                Description = attributeData.DESCRIPTION;



                if (attributeData == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "MovesCore at ID#" + ID);
                }
            }
        }


    }

}


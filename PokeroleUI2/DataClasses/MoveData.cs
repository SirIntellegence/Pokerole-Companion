using PokeroleUI2.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class MoveData
    {
        public int ID;
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Power { get; set; }
        public string PowerStat { get; set; }
        public string AccuracyStat1 { get; set; }
        public string AccuracyStat2 { get; set; }
        public string Target { get; set; }
        public string Effect { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public string RankString { get; set; }

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


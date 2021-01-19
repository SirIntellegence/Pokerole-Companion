using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeroleUI2.Databases;

namespace PokeroleUI2
{
    public class DexData
    {
        public ListData listData;
        public int ID { get { return listData.ID; } }
        public string DexID { get { return listData.DexID; } }
        public string Name { get { return listData.Name; } }
        public string Type1 { get { return listData.Type1; } }
        public string Type2 { get { return listData.Type2; } }
        public string Rank { get { return listData.Rank; } }

        public PkmnStatCollection Attributes;
        public float Height { get; set; }
        public float Weight { get; set; }
        public int BaseHP { get; set; }
        public string Description { get; set; }
        public List<string> Abilities;

        public DexData(ListData ld)
        {
            listData = ld;
            PopulateData();
        }

        private void PopulateData()
        {
            using (var db = new PokedexDBEntities())
            {
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
            }
        }

    }
}

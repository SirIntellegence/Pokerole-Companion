using PokeroleUI2.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class AbilityData
    {
        public string name { get; set; }
        public string desc { get; set; }

        public AbilityData(string name)
        {
            this.name = name;
            PopulateAbilityData();
        }

        private void PopulateAbilityData()
        {
            using (var db = new PokedexDBEntities())
            {
                var abilityData = (from d in db.AbilitiesDescs
                                   where d.Name == name
                                   select d).SingleOrDefault();

                if (abilityData == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "Abilities at " + name);
                }

                desc = abilityData.Description;
            }
        }
    }
}

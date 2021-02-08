using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class AbilityData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public AbilityData()
        {

        }
        /*
                private void PopulateAbilityData()
                {
                    using (var db = new PokedexDBEntities())
                    {
                        var abilityData = (from d in db.AbilitiesDescs
                                           where d.Name == Name
                                           select d).SingleOrDefault();

                        if (abilityData == null)
                        {
                            throw new System.ArgumentException("Parameter cannot be null", "Abilities at " + Name);
                        }

                        Description = abilityData.Description;
                    }
                }
                */
    }

}

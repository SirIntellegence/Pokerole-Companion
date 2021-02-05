using PokeroleUI2.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class LearnsetData
    {
        public int PokemonID;
        public List<MoveData> learnset { get; set; }

        public LearnsetData(int ID, int maxrank = 10)
        {
            PokemonID = ID;
            PopulateData(maxrank);
        }

        public MoveData GetByName(string n)
        {
            foreach(MoveData m in learnset)
            {
                if(n == m.Name)
                {
                    return m;
                }
            }
            return null;
        }

        private void PopulateData(int maxrank = 10)
        {
            learnset = new List<MoveData>();
            using (var db = new PokedexDBEntities())
            {
                var learnsetData = (from d in db.LearnsetsCores
                                     where d.Id == PokemonID
                                     select d).SingleOrDefault();

                string[] movenames = learnsetData.Moves.Split(',');
                string[] rankstrings = learnsetData.Ranks.Split(',');

                for(int i = 0; i < movenames.Length; i++)
                {
                    int rank = int.Parse(rankstrings[i]);
                    if(rank > maxrank) { continue; }
                    learnset.Add(new MoveData(movenames[i], rank));
                }

                if (learnsetData == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "LearnsetsCore at ID#" + PokemonID);
                }
            }
        }
    }
}

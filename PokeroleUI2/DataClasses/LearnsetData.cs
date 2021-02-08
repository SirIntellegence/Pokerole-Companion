using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class LearnsetData
    {
        public List<string> learnsetStrings { get; set; }
        public List<MoveData> learnset { get; set; }

        public LearnsetData(List<string> moves, int maxrank = 10)
        {
            learnsetStrings = moves;
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

            for (int i = 0; i < learnsetStrings.Count; i+=2)
            {
                int rank = 0;
                string movename = learnsetStrings[i];
                int.TryParse(learnsetStrings[i + 1], out rank);
                MoveData md = DataSerializer.LoadMoveData(movename);
                md.SetRank(rank);
                learnset.Add(md);
            }
        }

/*        private void PopulateData(int maxrank = 10)
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
        */
    }
}

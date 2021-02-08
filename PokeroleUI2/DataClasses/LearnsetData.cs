using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class LearnsetData
    {
        public int maxrank;
        public List<string> learnsetStrings { get; set; }
        public List<MoveData> learnset { get; set; }

        public LearnsetData(List<string> moves, int maxrank = 10)
        {
            this.maxrank = maxrank;
            learnsetStrings = moves;
            PopulateData();            
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

        private void PopulateData()
        {
            learnset = new List<MoveData>();

            for (int i = 0; i < learnsetStrings.Count - 1; i+=2)
            {
                int rank = 0;
                string movename = learnsetStrings[i];
                int.TryParse(learnsetStrings[i + 1], out rank);
                if(rank > maxrank)
                {
                    continue;
                }
                MoveData md = DataSerializer.LoadMoveData(movename);
                md.SetRank(rank);
                learnset.Add(md);
            }
        }
    }
}

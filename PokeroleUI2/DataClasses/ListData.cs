using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PokeroleUI2
{
    public class ListData
    {
        public int ID { get; set; }
        public string DexID { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string Rank { get; set; }
        public bool Starter { get; set; }
        public bool Exclude { get; set; }

        public ListData()
        {

        }

    }
}

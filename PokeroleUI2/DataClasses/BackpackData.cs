using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class BackpackData
    {
        public string MainPocket { get; set; }
        public string SmallPocket { get; set; }
        public List<PkmnSimpleStat> Potions { get; set; }
        public List<PkmnSimpleStat> SuperPotions { get; set; }
        public List<PkmnSimpleStat> HyperPotions { get; set; }
        public List<string> Badges;

    }
}

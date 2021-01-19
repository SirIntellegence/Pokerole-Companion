using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class PkmnSimpleStat
    {
        public int BaseVal { get; set; }
        public int Max { get; set; }
        public int Value { get; set; }

        public PkmnSimpleStat(int baseval, int max, int value)
        {
            BaseVal = baseval;
            Max = max;
            Value = value;
        }
    }
}

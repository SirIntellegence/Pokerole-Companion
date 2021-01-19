using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class PkmnStringStat
    {
        public string tag { get; set; }
        public string value { get; set; }

        public PkmnStringStat(string tag, string value)
        {
            this.tag = tag;
            this.value = value;
        }
    }
}

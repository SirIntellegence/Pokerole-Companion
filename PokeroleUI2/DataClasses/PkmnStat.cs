using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class PkmnStat
    {
        public string tag { get; set; }
        public int maxVal { get; set; }
        public int baseVal { get; set; }
        public int addVal { get; set; }

        public int AddVal { get { return addVal; } }
        public int MaxValue { get { return maxVal; } }
        public int Value { get { return baseVal + addVal; } }

        public bool CanIncrease { get { return Value < MaxValue; } }
        public bool CanDecrease { get { return Value > baseVal; } }

        public PkmnStat() { }

        public PkmnStat(string tag, int baseVal, int maxVal)
        {
            this.tag = tag;
            this.maxVal = maxVal;
            this.baseVal = baseVal;
        }

        public PkmnStat ShallowCopy()
        {
            return (PkmnStat)this.MemberwiseClone();
        }

        public int Adjust(int i, bool allowOverflow = false)
        {
            if (!allowOverflow && (Value + i > MaxValue || Value + i < baseVal))
            {
                addVal = Math.Max(addVal, 0); //reset addval to make sure it's not below zero
                return 0;
            }
            addVal += i;
            return i;
        }

        public int Clear()
        {
            int clearval = addVal;
            addVal = 0;
            return clearval;
        }

        public void SetMax(int i)
        {
            maxVal = i;
            if(Value > maxVal)
            {
                addVal = Math.Max(0, Value - maxVal);
            }
        }
    }
}

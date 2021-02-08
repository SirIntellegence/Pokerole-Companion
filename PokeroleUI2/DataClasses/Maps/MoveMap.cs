using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public sealed class MoveMap : ClassMap<MoveData>
    {
        public MoveMap()
        {
            Map(m => m.ID).Index(0).Name("ID");
            Map(m => m.Name).Index(1).Name("Name");
            Map(m => m.Type).Index(2).Name("Type");
            Map(m => m.Category).Index(3).Name("Category");
            Map(m => m.Power).Index(4).Name("Power");
            Map(m => m.PowerStat).Index(5).Name("PowerStat");
            Map(m => m.AccuracyStat1).Index(6).Name("AccuracyStat2");
            Map(m => m.AccuracyStat2).Index(7).Name("AccuracyStat1");
            Map(m => m.Target).Index(8).Name("Target");
            Map(m => m.Effect).Index(9).Name("Effect");
            Map(m => m.Description).Index(10).Name("Description");
        }

    }
}

using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public sealed class AbilityMap : ClassMap<AbilityData>
    {
        public AbilityMap()
        {
            Map(m => m.ID).Index(0).Name("ID");
            Map(m => m.Name).Index(1).Name("Name");
            Map(m => m.Description).Index(2).Name("Description");
        }
    }
}

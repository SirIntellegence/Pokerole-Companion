using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public sealed class ListMap : ClassMap<ListData>
    {
        public ListMap()
        {
            Map(m => m.ID).Index(0).Name("ID");
            Map(m => m.DexID).Index(1).Name("DexID");
            Map(m => m.Name).Index(2).Name("Name");
            Map(m => m.Type1).Index(3).Name("Type1");
            Map(m => m.Type2).Index(4).Name("Type2");
            Map(m => m.Rank).Index(5).Name("Rank");
            Map(m => m.Starter).Index(6).Name("Starter");
            Map(m => m.Exclude).Index(16).Name("Exclude");

        }
    }
}
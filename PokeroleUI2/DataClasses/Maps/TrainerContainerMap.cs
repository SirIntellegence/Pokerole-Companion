using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public sealed class TrainerContainerMap : ClassMap<TrainerContainer>
    {
        public TrainerContainerMap()
        {
            Map(m => m.rID).Index(0).Name("RID");
            Map(m => m.Name).Index(1).Name("Name");
            Map(m => m.Path).Index(2).Name("Path");
        }
    }
}

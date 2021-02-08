using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public sealed class DexMap : ClassMap<DexData>
    {
        public DexMap()
        {
            Map(m => m.ID).Index(0).Name("ID");
            Map(m => m.DexID).Index(1).Name("DexID");
            Map(m => m.Name).Index(2).Name("Name");
            Map(m => m.Type1).Index(3).Name("Type1");
            Map(m => m.Type2).Index(4).Name("Type2");
            Map(m => m.Rank).Index(5).Name("Rank");
            Map(m => m.Starter).Index(6).Name("Starter");
            Map(m => m.BaseHP).Index(7).Name("HP");
            Map(m => m.Height).Index(8).Name("Height");
            Map(m => m.Weight).Index(9).Name("Weight");
            Map(m => m.AttributeString).Index(10).Name("Attributes");
            Map(m => m.AbilitiesString).Index(11).Name("Abilities");
            Map(m => m.LearnsetString).Index(12).Name("Learnset");
            Map(m => m.Description).Index(13).Name("Description");
            Map(m => m.ImagePath).Index(14).Name("Img");
            Map(m => m.ThumbPath).Index(15).Name("Thumb");
            Map(m => m.Exclude).Index(16).Name("Exclude");

        }
    }
}

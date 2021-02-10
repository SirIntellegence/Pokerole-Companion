using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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


        public Brush typebrush1 { get { return PokemonUtils.GetTypeColour(Type1); } }
        public Brush typebrush2 { get { if (string.IsNullOrEmpty(Type2)) { return typebrush1; } return PokemonUtils.GetTypeColour(Type2); } }
        public BitmapImage RankImg { get { return PokemonUtils.GetRankImage(Rank); } }

        public ListData()
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PokeroleUI2
{
    public class MoveData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Brush typebrush { get { return PokemonUtils.GetTypeColour(Type); } }
        public string Category { get; set; }
        public BitmapImage CatImg { get { return PokemonUtils.GetCategoryImage(Category); } }

        public string Power { get; set; }
        public string PowerStat { get; set; }
        public string AccuracyStat1 { get; set; }
        public string AccuracyStat2 { get; set; }
        public string Target { get; set; }
        public string Effect { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public string RankString { get; set; }

        public string AccuracyString { get { return String.IsNullOrEmpty(AccuracyStat2) ? AccuracyStat1 : AccuracyStat1 + " + " + AccuracyStat2; } }
            
        public string DamageString
        {
            get { return String.IsNullOrEmpty(PowerStat) ? " - " : PowerStat + " + " + Power; }
        }



        public MoveData() {

        }

        public void SetRank(int rank = 0)
        {
            Rank = rank;
            RankString = PokemonUtils.RankFromInt(rank);
        }
    }

}


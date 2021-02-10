using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace PokeroleUI2
{
    public class MoveData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        private string _type = "";
        public string Type { get { return _type; } set { _type = value; } }
        public Brush typebrush { get { return PokemonUtils.GetTypeColour(Type); } }
        public Brush catbrush { get { return PokemonUtils.GetCategoryColour(Category); } }

        private string _category = "";
        public string Category { get { return _category; } set { _category = value; } }
        public BitmapImage CatImg { get { return PokemonUtils.GetCategoryImage(Category); } }
        public BitmapImage RankImg { get { return PokemonUtils.GetRankImage(Rank); } }

        public string Power { get; set; }
        public string PowerStat { get; set; }
        public string AccuracyStat1 { get; set; }
        public string AccuracyStat2 { get; set; }
        public string Target { get; set; }
        public string Effect { get; set; }
        public string Description { get; set; }
        private int _rank = int.MaxValue;
        public int Rank { get { return _rank; } set { _rank = value; } }
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


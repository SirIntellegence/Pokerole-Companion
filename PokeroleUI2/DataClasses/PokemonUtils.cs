using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using PokeroleUI2.Databases;

namespace PokeroleUI2
{
    public static class PokemonUtils
    {


        public static void SetTypeColours(string t1, string t2 = "")
        {
            Application.Current.Resources["Col_Type1"] = Application.Current.Resources["Col_" + t1];
            if (string.IsNullOrEmpty(t2)) { t2 = t1; }
            Application.Current.Resources["Col_Type2"] = Application.Current.Resources["Col_" + t2];
        }

        public static void SetMoveColours(string t)
        {
            Application.Current.Resources["Col_MoveType"] = Application.Current.Resources["Col_" + t];
        }

        public static string GetTypeColour(string t)
        {
            string tag = "Col_" + t;
            return t;
        }


        public static int RankFromString(string r)
        {
            return (int)((RANKS)Enum.Parse(typeof(RANKS), r));
        }

        public static string RankFromInt(int r)
        {
            return Enum.GetName(typeof(RANKS), r);
        }

        public static int GetAttributePointMax(int rank)
        {
            switch (rank)
            {
                case 0:
                    return 0;
                case 1:
                    return 2;
                case 2:
                    return 4;
                case 3:
                    return 6;
                case 4:
                    return 8;
                case 5:
                    return 8;
                case 6:
                    return 14;
            }
            return 0;
        }

        public static int GetSkillPointMax(int rank)
        {
            switch (rank)
            {
                case 0:
                    return 5;
                case 1:
                    return 9;
                case 2:
                    return 12;
                case 3:
                    return 14;
                case 4:
                    return 15;
                case 5:
                    return 15;
                case 6:
                    return 16;
            }
            return 0;

        }

        public static int GetSocialPointMax(int rank)
        {
            switch (rank)
            {
                case 0:
                    return 0;
                case 1:
                    return 2;
                case 2:
                    return 4;
                case 3:
                    return 6;
                case 4:
                    return 8;
                case 5:
                    return 14;
                case 6:
                    return 14;
            }
            return 0;
        }

        public static int GetSkillCap(int rank)
        {
            return Math.Min(rank + 1, 5);
        }

        public static string GetImagePath(string dexID)
        {
            string filename = "001_Bulbasaur_Dream.png";
            using (var db = new PokedexDBEntities())
            {
                var imageData = (from d in db.ImagePaths
                                 where d.DexID == dexID
                                 select d).SingleOrDefault();

                if(imageData == null)
                {
                    return filename;
                }
                string[] filenames = imageData.FullPaths.Split(',');
                if (!String.IsNullOrEmpty(filenames[0])) { filename = filenames[0]; }

                if (imageData == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "Images at ID#" + dexID);
                }
            }
            return filename;
        }
    }
}

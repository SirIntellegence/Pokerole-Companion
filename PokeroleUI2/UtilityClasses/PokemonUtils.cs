using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using System.Configuration;

namespace PokeroleUI2
{
    public static class PokemonUtils
    {

        public static BitmapImage GetCategoryImage(string c)
        {
            c = FirstToUpper(c);
            string path = "pack://application:,,,/Graphics/Icons/Category_";
            Uri uri = new Uri(path + c + ".png");
            try
            {
                return new BitmapImage(uri);
            }
            catch
            {
            }
            return null;
        }

        public static Brush GetCategoryColour(string t)
        {
            if (t == null) { return null; }
            t = FirstToUpper(t);
            string tag = "Col_Cat_" + t;
            return (Brush)Application.Current.Resources[tag];
        }


        public static BitmapImage GetRankImage(int r)
        {
            return GetRankImage(RankFromInt(r));
        }

        public static BitmapImage GetRankImage(string r)
        {
            r = FirstToUpper(r);
            string path = "pack://application:,,,/Graphics/Icons/Rank_";
            Uri uri = new Uri(path + r + ".png");
            try
            {
                return new BitmapImage(uri);
            }
            catch
            {
            }
            return null;
        }


        public static Brush GetTypeColour(string t)
        {
            if (t == null) { return null; }
            t = FirstToUpper(t);
            string tag = "Col_" + t;
            return (Brush)Application.Current.Resources["Col_" + t];
        }

        public static string FirstToUpper(string s)
        {
            if (string.IsNullOrEmpty(s)) { return string.Empty; }
            s = s.ToLower();
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
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

        public static BitmapImage GetPkmnImage(string filename)
        {
            string path = Path.GetFullPath(ConfigurationManager.AppSettings["SpriteDirectory"] + filename);
            Uri uri = new Uri(path);
            try
            {
                return new BitmapImage(uri);
            }
            catch
            {
            }
            return null;
        }
       
    }

}

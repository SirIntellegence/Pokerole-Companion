using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using PokeroleUI2.Databases;
using System.Windows.Media;

namespace PokeroleUI2
{
    public static class PokemonUtils
    {

        public static BitmapImage GetCategoryImage(string c)
        {
            c = FirstToUpper(c);
            string path = "pack://application:,,,/Graphics/Icons/Category_";
            return new BitmapImage(new Uri(path + c + ".png"));
        }

        public static void SetTypeColours(string t1, string t2 = "")
        {
            Application.Current.Resources["Col_Type1"] = Application.Current.Resources["Col_" + t1];
            if (string.IsNullOrEmpty(t2)) { t2 = t1; }
            Application.Current.Resources["Col_Type2"] = Application.Current.Resources["Col_" + t2];
        }


        public static Brush GetTypeColour(string t)
        {
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

        public static string GetPkmnImagePath(string dexID)
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

        public static BitmapImage GetPkmnImage(string dexID)
        {
            string filename = "001_Bulbasaur_Dream.png";
            using (var db = new PokedexDBEntities())
            {
                var imageData = (from d in db.ImagePaths
                                 where d.DexID == dexID
                                 select d).SingleOrDefault();

                if (imageData == null)
                {
                    return new BitmapImage();
                }
                string[] filenames = imageData.FullPaths.Split(',');
                if (!String.IsNullOrEmpty(filenames[0])) { filename = filenames[0]; }

                if (imageData == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "Images at ID#" + dexID);
                }
            }
            return new BitmapImage(new Uri("pack://application:,,,/Graphics/Sprites/FullRes/" + filename) );
        }

        public static float[] HSVToRGB(float[] color)
        {
            float[] rgb = new float[3];
            float[] col = new float[] { color[0], color[1], color[2] };

            if (col[1] == 0.0f)
            {
                rgb[0] = col[2];
                rgb[1] = col[2];
                rgb[1] = col[2];
            }
            else
            {
                float h = col[0] * 6.0f;

                if (h == 6.0f)
                {
                    h = 0.0f;
                }

                int v_i = (int)h;
                float v_1 = col[2] * (1.0f - col[1]);
                float v_2 = col[2] * (1.0f - (col[1] * (h - v_1)));
                float v_3 = col[2] * (1.0f - (col[1] * (1.0f - (h - v_i))));

                if (v_i == 0)
                {
                    rgb[0] = col[2];
                    rgb[1] = v_3;
                    rgb[2] = v_1;
                }
                else if (v_i == 1)
                {
                    rgb[0] = v_2;
                    rgb[1] = col[2];
                    rgb[2] = v_1;
                }
                else if (v_i == 2)
                {
                    rgb[0] = v_2;
                    rgb[1] = col[2];
                    rgb[2] = v_3;
                }
                else if (v_i == 3)
                {
                    rgb[0] = v_1;
                    rgb[1] = v_2;
                    rgb[2] = col[2];
                }
                else if (v_i == 4)
                {
                    rgb[0] = v_3;
                    rgb[1] = v_1;
                    rgb[2] = col[2];
                }
                else
                {
                    rgb[0] = col[2];
                    rgb[1] = v_1;
                    rgb[2] = v_2;
                }
            }

            return new float[] { rgb[0], rgb[1], rgb[2]};
        }

    }

}

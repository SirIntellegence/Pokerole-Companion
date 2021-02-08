using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace PokeroleUI2
{
    public class TrainerContainer : INotifyPropertyChanged
    {
        public long rID { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value;
                OnPropertyChanged();
            }
        }
        public string Path { get; set; }

        public TrainerContainer()
        {
        }

        public TrainerContainer(TrainerData td)
        {
            rID = td.rID;
            Name = td.Name;
            Path = td.path;
        }

        public TrainerData LoadTrainer()
        {
            TrainerData trainer = (TrainerData)DataSerializer.LoadXML(Path, typeof(TrainerData));
            return trainer;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class TrainerData
    {
        [XmlIgnoreAttribute]
        public TrainerContainer Container;
        [XmlIgnoreAttribute]
        public string path { get { return ConfigurationManager.AppSettings["TrainerDirectory"] + rID + ".xml"; } }
        public long rID;
        public float displaycolor;
        [XmlIgnoreAttribute]
        private SolidColorBrush _displayBrush;
        [XmlIgnoreAttribute]
        public SolidColorBrush displayBrush
        {
            get
            {
                if(_displayBrush == null)
                {
                    float h = displaycolor;
                    float s = 0.75f;
                    float l = 0.75f;
                    float[] rgb = PokemonUtils.HSVToRGB(new float[] { h, s, l });
                    Color color = Color.FromScRgb(1, rgb[0], rgb[1], rgb[2]);
                    _displayBrush = new SolidColorBrush(color);
                    Debug.WriteLine("Color" + color.R + color.G + color.B);
                }

                return _displayBrush;
            }
        }

        public PkmnStatCollection Attributes;
        public PkmnStatCollection SocialAttributes;
        public PkmnStatCollection Skills;

        public string _name;
        public string Name {
            get { return _name; }
            set { _name = value; if (Container != null) { Container.Name = value; } }
        }
        public string PlayerName { get; set; }
        public string Concept { get; set; }


        public int Age { get; set; }
        public int Rank { get; set; }
        [XmlIgnoreAttribute]
        public string RankString { get { return PokemonUtils.RankFromInt(Rank); } }

        public string RankAchievements { get; set; }

        public PkmnSimpleStat HP { get; set; }
        public PkmnSimpleStat Will { get; set; }


        public string Nature { get; set; }
        public int Confidence { get; set; }

        public int Caught { get; set; }
        public int Seen { get; set; }

        public string imgpath { get; set; }

        public BackpackData Backpack;
        public float Money { get; set; }

        private ObservableCollection<PokemonData> _party;
        public ObservableCollection<PokemonData> Party { get { return _party; } set { _party = value;} }
        public ObservableCollection<PokemonData> Box { get; set; }
        [XmlIgnoreAttribute]
        public int PkmnCount { get { return Party.Count + Box.Count; } }

        [XmlIgnoreAttribute]
        public PokemonData ActivePokemon;


        public TrainerData()
        {           
        }

        public TrainerData(string name)
        {
            rID = GenerateRID();
            Rank = 0;
            Name = name;
            Party = new ObservableCollection<PokemonData>();
            Box = new ObservableCollection<PokemonData>();
            PopulateStats();
            UpdateDependencies();

            float h = displaycolor;
            float s = 0.75f;
            float l = 0.75f;
            float[] rgb = PokemonUtils.HSVToRGB(new float[] { h, s, l });
            Color color = Color.FromScRgb(1, rgb[0], rgb[1], rgb[2]);
            _displayBrush = new SolidColorBrush(color);
        }


        public long GenerateRID()
        {
            Random rnd = new Random();
            byte[] buf = new byte[8];
            rnd.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            long result = (Math.Abs(longRand % (2000000000000000 - 1000000000000000)) + 1000000000000000);

            long random_seed = (long)rnd.Next(1000, 5000);
            random_seed = random_seed * result + rnd.Next(1000, 5000);
            long result2 = ((long)(random_seed / 655) % 10000000000000001);
            displaycolor = Math.Abs(result2) / long.MaxValue;

            return result2;
        }

        public void PopulateStats()
        {
            PkmnStat str = new PkmnStat("Strength", 1, 5);
            PkmnStat dex = new PkmnStat("Dexterity", 1, 5);
            PkmnStat vit = new PkmnStat("Vitality", 1, 5);
            PkmnStat ins = new PkmnStat("Insight", 1, 5);
            Attributes = new PkmnStatCollection(new List<PkmnStat> { str, dex, vit, ins }, 0, 0);

            PkmnStat tou = new PkmnStat("Tough", 1, 5);
            PkmnStat coo = new PkmnStat("Cool", 1, 5);
            PkmnStat bea = new PkmnStat("Beauty", 1, 5);
            PkmnStat cle = new PkmnStat("Clever", 1, 5);
            PkmnStat cut = new PkmnStat("Cute", 1, 5);
            SocialAttributes = new PkmnStatCollection(new List<PkmnStat> { tou, coo, bea, cle, cut},0,0);

            PkmnStat brawl = new PkmnStat("Brawl", 0, 5);
            PkmnStat threw = new PkmnStat("Throw", 0, 5);
            PkmnStat evade = new PkmnStat("Evasion", 0, 5);
            PkmnStat weapo = new PkmnStat("Weapons", 0, 5);
            PkmnStat alert = new PkmnStat("Alert", 0, 5);
            PkmnStat athle = new PkmnStat("Athletics", 0, 5);
            PkmnStat natur = new PkmnStat("Nature", 0, 5);
            PkmnStat steal = new PkmnStat("Stealth", 0, 5);
            PkmnStat allur = new PkmnStat("Allure", 0, 5);
            PkmnStat etiqu = new PkmnStat("Etiquette", 0, 5);
            PkmnStat intim = new PkmnStat("Intimidate", 0, 5);
            PkmnStat perfo = new PkmnStat("Performance", 0, 5);
            PkmnStat craft = new PkmnStat("Crafts", 0, 5);
            PkmnStat lore = new PkmnStat("Lore", 0, 5);
            PkmnStat medic = new PkmnStat("Medicine", 0, 5);
            PkmnStat scien = new PkmnStat("Science", 0, 5);

            Skills = new PkmnStatCollection(new List<PkmnStat> { brawl, threw, evade, weapo, alert, athle, natur, steal, allur, etiqu, intim, perfo, craft, lore, medic, scien }, 0, 0);

            HP = new PkmnSimpleStat(Attributes.GetStatByTag("Vitality").Value, Attributes.GetStatByTag("Vitality").Value, Attributes.GetStatByTag("Vitality").Value);
            Will = new PkmnSimpleStat(0, Attributes.GetStatByTag("Insight").Value + 2, Attributes.GetStatByTag("Insight").Value + 2);

        }

        public void UpdateDependencies()
        {
            HP.Max = Attributes.GetStatByTag("Vitality").Value;
            Will.Max = Attributes.GetStatByTag("Insight").Value + 2;

            int ageattrmod = 0;
            int agesocimod = 0;
            if(Age > 10)
            {
                ageattrmod = 2;
                agesocimod = 2;
            }

            if (Age > 19)
            {
                ageattrmod = 4;
                agesocimod = 4;
            }

            if (Age > 50)
            {
                ageattrmod = 3;
                agesocimod = 6;
            }

            Attributes.MaxPoints = Math.Min(Rank * 2, 8) + ageattrmod;
            SocialAttributes.MaxPoints = Math.Min(Rank * 2, 8) + agesocimod;

            Skills.SetAllMax(PokemonUtils.GetSkillCap(Rank));
            Skills.MaxPoints = (PokemonUtils.GetSkillPointMax(Rank));
        }

        public void ShiftParty()
        {
            if(_party == null) { return; }
            List<PokemonData> shifts = new List<PokemonData>();
            foreach (PokemonData pd in _party.Skip(6))
            {
                shifts.Add(pd);
            }
            foreach(PokemonData pd in shifts)
            {
                _party.Remove(pd);
                Box.Insert(0, pd);                
            }
        }

        public void AddPokemon(DexData dd)
        {
            PokemonData pd = new PokemonData(dd);
            Party.Add(pd);
            ShiftParty();
        }

        public void DeletePokemon(PokemonData pd)
        {
            if (Party.Contains(pd))
            {
                Party.Remove(pd);
            }
            if (Box.Contains(pd))
            {
                Box.Remove(pd);
            }
        }

        public void Save()
        {
            DataSerializer.SaveXML(this, path, GetType());
        }
    }
}

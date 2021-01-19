using PokeroleUI2.Databases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PokeroleUI2
{
    public class PokemonData
    {
        public int PokemonID;
        public string dexID { get; set; }
        public string dexName { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int Rank { get; set; }
        public string TextRank { get; set; }

        public float Height { get; set; }
        public float Weight { get; set; }

        public PkmnStatCollection Attributes;
        public PkmnStatCollection SocialAttributes;
        public PkmnStatCollection Skills;


        public int Evasion { get; set; }
        public int Clash { get; set; }
        public int DEFSPDEF { get; set; }

        public PkmnStat Happiness { get; set; }
        public PkmnStat Loyalty { get; set; }
        public int Battles { get; set; }
        public int Victories { get; set; }

        public PkmnStringStat Nature { get; set; }
        public PkmnStringStat Confidence { get; set; }
        public PkmnStringStat Status { get; set; }
        public PkmnStringStat Accessory { get; set; }
        public PkmnStringStat Ribbons { get; set; }
        public PkmnStringStat Item { get; set; }

        public PkmnSimpleStat HP { get; set; }
        public PkmnSimpleStat Will { get; set; }

        public List<string> Abilities;
        int LearnableMoves = 4;
        public List<MoveData> Moves;

        private LearnsetData _learnSet;
        public LearnsetData LearnSet
        {
            get
            {
                if (_learnSet == null) { _learnSet = new LearnsetData(PokemonID, Rank); }
                return _learnSet;        
            }
            set
            {
                _learnSet = value;
            }
        }

        public string Weaknesses;

        public PokemonData(DexData dd)
        {
            PokemonID = dd.ID;
            dexID = dd.DexID;
            dexName = dd.Name;
            Name = dd.Name;
            Type1 = dd.Type1;
            Type2 = dd.Type2;
            Rank = PokemonUtils.RankFromString(dd.Rank);
            Weight = dd.Weight;
            Height = dd.Height;
            Abilities = dd.Abilities;
            Moves = new List<MoveData>();
            PopulateStats(dd);
            UpdateDependencies();
        }

        public void PopulateStats(DexData dd)
        {
            Attributes = dd.Attributes.DeepCopy();

            PkmnStat TOU = new PkmnStat("Tough", 1, 5);
            PkmnStat COO = new PkmnStat("Cool", 1, 5);
            PkmnStat BEA = new PkmnStat("Beauty", 1, 5);
            PkmnStat CUT = new PkmnStat("Cute", 1, 5);
            PkmnStat CLE = new PkmnStat("Clever", 1, 5);
            List<PkmnStat> socattList = new List<PkmnStat> { TOU, COO, BEA, CUT, CLE };
            SocialAttributes = new PkmnStatCollection(socattList, 0, 0);

            PkmnStat BRAWL = new PkmnStat("Brawl", 0, 1);
            PkmnStat CHANN = new PkmnStat("Channel", 0, 1);
            PkmnStat CLASH = new PkmnStat("Clash", 0, 1);
            PkmnStat EVADE = new PkmnStat("Evasion", 0, 1);
            PkmnStat ALERT = new PkmnStat("Alert", 0, 1);
            PkmnStat ATHLE = new PkmnStat("Athletic", 0, 1);
            PkmnStat NATUR = new PkmnStat("Nature", 0, 1);
            PkmnStat STEAL = new PkmnStat("Stealth", 0, 1);
            PkmnStat ALLUR = new PkmnStat("Allure", 0, 1);
            PkmnStat ETIQU = new PkmnStat("Etiquette", 0, 1);
            PkmnStat INTIM = new PkmnStat("Intimidate", 0, 1);
            PkmnStat PERFO = new PkmnStat("Perform", 0, 1);
            List<PkmnStat> skillList = new List<PkmnStat> { BRAWL, CHANN, CLASH, EVADE, ALERT, ATHLE, NATUR, STEAL, ALLUR, ETIQU, INTIM, PERFO };
            Skills = new PkmnStatCollection(skillList, 0, 0);

            Happiness = new PkmnStat("Happiness", 0, 5);
            Loyalty = new PkmnStat("Happiness", 0, 5);

            Item = new PkmnStringStat("Item", "Item");
            Nature = new PkmnStringStat("Nature", "Nature");
            Confidence = new PkmnStringStat("Confidence", "Confidence");
            Status = new PkmnStringStat("Status", "Status");
            Accessory = new PkmnStringStat("Accessory", "Accessory");
            Ribbons = new PkmnStringStat("Ribbons", "Ribbons");

            HP = new PkmnSimpleStat(dd.BaseHP, dd.BaseHP, dd.BaseHP);
            Will = new PkmnSimpleStat(2, Attributes.GetStatByTag("Insight").Value + 2, Attributes.GetStatByTag("Insight").Value + 2);


        }

        public void UpdateDependencies()
        {
            TextRank = PokemonUtils.RankFromInt(Rank);
            SocialAttributes.MaxPoints = PokemonUtils.GetSocialPointMax(Rank);
            Attributes.MaxPoints = PokemonUtils.GetAttributePointMax(Rank);
            Skills.MaxPoints = PokemonUtils.GetSkillPointMax(Rank);
            Skills.SetAllMax(PokemonUtils.GetSkillCap(Rank));
            HP.Max = HP.BaseVal + Attributes.GetStatByTag("Vitality").Value;
            Will = new PkmnSimpleStat(2, Attributes.GetStatByTag("Insight").Value + 2, Attributes.GetStatByTag("Insight").Value + 2);
            LearnSet = new LearnsetData(PokemonID, Rank);
            ReLengthMovesList();
        }

        public void ReLengthMovesList()
        {
            LearnableMoves = Attributes.GetStatByTag("Insight").Value + 2;
            List<MoveData> theseMoves = new List<MoveData>();
            for (int i = 0; i < LearnableMoves; i++)
            {
                if(Moves.Count > i)
                {
                    theseMoves.Add(Moves[i]);
                    continue;
                }
                theseMoves.Add(LearnSet.learnset[0]);
            }
            Moves = theseMoves;

        }
    }
}

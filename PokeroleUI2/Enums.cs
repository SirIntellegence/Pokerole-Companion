using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public enum RANKS
    {
        Starter = 0,
        Beginner = 1,
        Amateur = 2,
        Ace = 3,
        Pro = 4,
        Master = 5,
        Champion = 6
    }

    enum STAT_ATTRIBUTES
    {
        Strength,
        Dexterity,
        Vitality,
        Special,
        Insight,
    }
    enum STAT_SOCIALATTRIBUTES
    {
        Tough,
        Cool,
        Beauty,
        Cute,
        Clever
    }

    enum STAT_SKILLS
    {
        //Fight
        Brawl,
        Channel,
        Clash,
        Evasion,
        //Survival
        Alert,
        Athletic,
        Nature,
        Stealth,
        //Social
        Allure,
        Etiquette,
        Intimidate,
        Perform
    }

    enum PKMNTYPE
        {
            Bug,
            Dark,
            Dragon,
            Electric,
            Fairy,
            Fight,
            Fire,
            Flying,
            Ghost,
            Grass,
            Ground,
            Ice,
            Normal,
            Poison,
            Psychic,
            Rock,
            Steel,
            Water
        }

        enum MOVECATEGORY
        {
            Physical,
            Special,
            Support
        }
}

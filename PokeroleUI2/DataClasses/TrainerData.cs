using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class TrainerData
    {
        public string Name { get; set; }
        public List<PokemonData> Party { get; set; }
        public PokemonData ActivePokemon;

        public TrainerData(string name)
        {
            Name = name;
            Party = new List<PokemonData>();
        }

        public void CatchPokemon(DexData dd)
        {
            PokemonData pd = new PokemonData(dd);
            Party.Add(pd);
            ActivePokemon = pd;
        }
    }
}

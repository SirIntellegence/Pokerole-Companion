using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public class TrainerList
    {
        public List<TrainerSave> Trainers;

        public TrainerList()
        {

        }

        public TrainerList(List<TrainerSave> list)
        {
            Trainers = list;
        }
    }

    public class TrainerSave
    {
        public string Name;
        public string Path;
    }
}

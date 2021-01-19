using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeroleUI2.Databases;


namespace PokeroleUI2
{
    public class ListData
    {
        public int ID;
        public string DexID { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string Rank { get; set; }

        public ListData(int id)
        {
            this.ID = id;
            PopulateData();
        }

        private void PopulateData()
        {
            using (var db = new PokedexDBEntities())
            {
                var data = (from d in db.DexCores
                            where d.Id == ID
                            select d).SingleOrDefault();

                if (data == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "data");
                }

                DexID = data.DEXID;
                Name = data.NAME;
                Type1 = data.TYPE1;
                Type2 = data.TYPE2;
                Rank = data.RANK;
            }
        }
    }
}

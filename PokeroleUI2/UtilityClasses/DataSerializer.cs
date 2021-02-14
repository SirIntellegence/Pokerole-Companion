using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;

namespace PokeroleUI2
{
    public static class DataSerializer
    {
        public static void SaveXML(Object file, string path, Type type)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            XmlSerializer x = new System.Xml.Serialization.XmlSerializer(type);
            using (StreamWriter writer = new StreamWriter(path))
            {
                x.Serialize(writer, file);
            }
        }

        public static object LoadXML(string path, Type type)
        {
            XmlSerializer x = new System.Xml.Serialization.XmlSerializer(type);
            using (var reader = new StreamReader(path))
            {
                Object file;
                file = x.Deserialize(reader);
                return file;
            }
        }

        public static void DeleteTrainer(TrainerContainer tc)
        {
            string path = Path.GetFullPath(tc.Path);
            File.Delete(path);
            Debug.WriteLine(path);
        }

        public static void SaveTrainerContainers(IEnumerable<TrainerContainer> TrainerContainers)
        {
            string path = ConfigurationManager.AppSettings["TrainerContainerPath"];
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<TrainerContainerMap>();
                csv.WriteHeader<TrainerContainer>();
                csv.NextRecord();
                foreach (var record in TrainerContainers)
                {
                    csv.WriteRecord(record);
                    csv.NextRecord();
                }
            }
        }

        public static ObservableCollection<TrainerContainer> LoadAllTrainerContainers()
        {
            string path = ConfigurationManager.AppSettings["TrainerContainerPath"];

            ObservableCollection<TrainerContainer> tcs;
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<TrainerContainerMap>();
                tcs = new ObservableCollection<TrainerContainer>(csv.GetRecords<TrainerContainer>());

            }
            return tcs;
        }

        public static void SaveDexData(List<DexData> dds)
        {
            string path = ConfigurationManager.AppSettings["DexPath"];
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<DexMap>();
                csv.WriteHeader<DexData>();
                csv.NextRecord();
                foreach (var record in dds)
                {
                    csv.WriteRecord(record);
                    csv.NextRecord();
                }
            }
        }

        public static ObservableCollection<DexData> LoadAllDexData()
        {
            string path = ConfigurationManager.AppSettings["DexPath"];

            ObservableCollection<DexData> dds;
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<DexMap>();
                dds = new ObservableCollection<DexData>(csv.GetRecords<DexData>());
                
            }
            return dds;
        }

        public static DexData LoadDexData(int ID)
        {
            string path = ConfigurationManager.AppSettings["DexPath"];

            DexData dd;
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                throw new Exception("Dex CSV not found");
            }

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<DexMap>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if(csv.GetField<int>("ID") == ID)
                    {
                        dd = csv.GetRecord<DexData>();
                        return dd;
                    }
                }
            }
            throw new Exception("No dexdata found at ID" + ID);
        }

        public static ObservableCollection<ListData> LoadAllListData()
        {
            string path = ConfigurationManager.AppSettings["DexPath"];

            ObservableCollection<ListData> lds;
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                return null;
            }

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<ListMap>();
                lds = new ObservableCollection<ListData>(csv.GetRecords<ListData>());

            }
            return lds;
        }

        public static MoveData LoadMoveData(string name)
        {
            string path = ConfigurationManager.AppSettings["MovesPath"];
            MoveData md;
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                throw new Exception("Move CSV not found");
            }

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<MoveMap>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (csv.GetField<string>("Name") == name)
                    {
                        md = csv.GetRecord<MoveData>();
                        return md;
                    }
                }
            }
            return new MoveData();
            //throw new Exception("No movedata found at name: " + name); 
        }

        public static AbilityData LoadAbilityData(string name)
        {
            string path = ConfigurationManager.AppSettings["AbilitiesPath"];
            AbilityData ad;
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                throw new Exception("Ability CSV not found");
            }

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<AbilityMap>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (csv.GetField<string>("Name") == name)
                    {
                        ad = csv.GetRecord<AbilityData>();
                        return ad;
                    }
                }
            }
            return null;
            //throw new Exception("No abilitydata found at name: " + name);
        }
    }
}

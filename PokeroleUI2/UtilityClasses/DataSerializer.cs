using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeroleUI2
{
    public static class DataSerializer
    {
        public static void Save(Object file, string path, Type type)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            XmlSerializer x = new System.Xml.Serialization.XmlSerializer(type);
            StreamWriter writer = new StreamWriter(path);
            x.Serialize(writer, file);
            writer.Close();
        }

        public static object Load(string path, Type type)
        {
            XmlSerializer x = new System.Xml.Serialization.XmlSerializer(type);
            StreamReader reader = new StreamReader(path);
            Object file;
            file = x.Deserialize(reader);
            reader.Close();
            return file;
        }
    }
}

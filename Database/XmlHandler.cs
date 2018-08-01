using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MusicLog
{
    public class XmlHandler
    {
        public void Serialize<T>(T obj, string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            TextWriter writer = new StreamWriter(fileName);
            ser.Serialize(writer, obj);
            writer.Close();
        }

        public void Deserialize<T>(T container, string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            StreamReader reader = new StreamReader(filePath);
            container = (T)ser.Deserialize(reader);
            reader.Close();
        }
        
    }
}

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

        public void Serialize(MusicObjectTable table, string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(MusicObjectTable));
            TextWriter writer = new StreamWriter(fileName);
            ser.Serialize(writer, table);
            writer.Close();
        }

        public MusicObjectTable Deserialize(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(MusicObjectTable));
            StreamReader reader = new StreamReader(filePath);
            MusicObjectTable musicTable = new MusicObjectTable();
            musicTable = (MusicObjectTable)ser.Deserialize(reader);
            reader.Close();
            return musicTable;
        }
    }
}

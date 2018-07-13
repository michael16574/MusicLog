using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MusicLog
{
    public class UserSettings
    {
        public string ExeDirectory;

        public string DefaultDatabasePath;
        public string DefaultCredentialsPath;

        public string DatabasePath;
        public string CredentialsPath;

        public Credentials Creds;

        public UserSettings()
        {
            ExeDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            DefaultDatabasePath = ExeDirectory + "\\database.xml";
            DefaultCredentialsPath = ExeDirectory + "\\credentials.xml";
            DatabasePath = DefaultDatabasePath;
            CredentialsPath = DefaultCredentialsPath;
            LoadCredentials();
        }
        public UserSettings(string databasePath, string credentialsPath)
        {
            ExeDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            DefaultDatabasePath = ExeDirectory + "\\database.xml";
            DefaultCredentialsPath = ExeDirectory + "\\credentials.xml";
            DatabasePath = databasePath;
            CredentialsPath = credentialsPath;
            LoadCredentials();
        }

        public void LoadCredentials()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Credentials));
            StreamReader reader = new StreamReader(CredentialsPath);
            Creds = new Credentials();
            Creds = (Credentials)ser.Deserialize(reader);
            reader.Close();
        }

        public void SaveCredentials(Credentials newCred)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Credentials));
            TextWriter writer = new StreamWriter(CredentialsPath);
            ser.Serialize(writer, newCred);
            writer.Close();
        }
    }
}

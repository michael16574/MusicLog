using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MusicLog
{
    public class MusicObjectTable : IXmlSerializable
    {
        public List<IArtist> Artists;
        public List<IAlbum> Albums;
        public List<ITrack> Tracks;

        public MusicObjectTable()
        {
            this.Artists = new List<IArtist>();
            this.Albums = new List<IAlbum>();
            this.Tracks = new List<ITrack>();
        }

        public MusicObjectTable(List<IArtist> artists, List<IAlbum> albums, List<ITrack> tracks)
        {
            this.Artists = artists;
            this.Albums = albums;
            this.Tracks = tracks;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("MusicObjectTable");
            ReadXmlArtists(reader);
            ReadXmlAlbums(reader);
            ReadXmlTracks(reader);

        }      

        public void WriteXml(XmlWriter writer)
        {
            WriteXmlArtists(writer);
            WriteXmlAlbums(writer);
            WriteXmlTracks(writer);

        }

        private void ReadXmlArtists(XmlReader reader)
        {
            reader.ReadStartElement("IArtists");
            if (reader.IsStartElement("IArtist"))
            {
                while (reader.IsStartElement("IArtist"))
                {
                    Type type = Type.GetType(reader.GetAttribute("AssemblyQualifiedName"));
                    XmlSerializer serial = new XmlSerializer(type);
                    reader.ReadStartElement("IArtist");
                    this.Artists.Add((IArtist)serial.Deserialize(reader));
                    reader.ReadEndElement();
                }
            }
            reader.ReadEndElement();
        }

        private void ReadXmlAlbums(XmlReader reader)
        {
            reader.ReadStartElement("IAlbums");
            if (reader.IsStartElement("IAlbum"))
            {
                while (reader.IsStartElement("IAlbum"))
                {
                    Type type = Type.GetType(reader.GetAttribute("AssemblyQualifiedName"));
                    XmlSerializer serial = new XmlSerializer(type);
                    reader.ReadStartElement("IAlbum");
                    this.Albums.Add((IAlbum)serial.Deserialize(reader));
                    reader.ReadEndElement();
                }
            }
            reader.ReadEndElement();
        }

        private void ReadXmlTracks(XmlReader reader)
        {
            reader.ReadStartElement("ITracks");
            if (reader.IsStartElement("ITrack"))
            {
                while (reader.IsStartElement("ITrack"))
                {
                    Type type = Type.GetType(reader.GetAttribute("AssemblyQualifiedName"));
                    XmlSerializer serial = new XmlSerializer(type);
                    reader.ReadStartElement("ITrack");
                    this.Tracks.Add((ITrack)serial.Deserialize(reader));
                    reader.ReadEndElement();
                }
            }
            reader.ReadEndElement();
        }

        private void WriteXmlArtists(XmlWriter writer)
        {
            writer.WriteStartElement("IArtists");
            foreach (IArtist artist in Artists)
            {
                writer.WriteStartElement("IArtist");
                writer.WriteAttributeString("AssemblyQualifiedName", artist.GetType().AssemblyQualifiedName);
                XmlSerializer xmlSerializer = new XmlSerializer(artist.GetType());
                xmlSerializer.Serialize(writer, artist);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteXmlAlbums(XmlWriter writer)
        {
            writer.WriteStartElement("IAlbums");
            foreach (IAlbum album in Albums)
            {
                writer.WriteStartElement("IAlbum");
                writer.WriteAttributeString("AssemblyQualifiedName", album.GetType().AssemblyQualifiedName);
                XmlSerializer xmlSerializer = new XmlSerializer(album.GetType());
                xmlSerializer.Serialize(writer, album);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        
        private void WriteXmlTracks(XmlWriter writer)
        {
            writer.WriteStartElement("ITracks");
            foreach (ITrack track in Tracks)
            {
                writer.WriteStartElement("ITrack");
                writer.WriteAttributeString("AssemblyQualifiedName", track.GetType().AssemblyQualifiedName);
                XmlSerializer xmlSerializer = new XmlSerializer(track.GetType());
                xmlSerializer.Serialize(writer, track);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}

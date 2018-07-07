using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MusicLog.Database
{
    /// <summary>
    /// Contains database and methods pertaining to modification of such.
    /// </summary>
    public class DatabaseInstance
    { 
        public DatabaseInstance()
        {
            Artists = new List<Artist>();
        }
        public DatabaseInstance(string location)
        {
            DeserializeDatabase(location);
        }

        public void SerializeDatabase(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Artist>));
            TextWriter writer = new StreamWriter(fileName);            
            ser.Serialize(writer, Artists);
            writer.Close();
        }       
        public void DeserializeDatabase(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Artist>));
            StreamReader reader = new StreamReader(filePath);
            Artists = (List<Artist>)ser.Deserialize(reader);
            reader.Close();
        }

        public void AddArtist(string name, string id)
        {
            Artist NewArtist = new Artist();
            NewArtist.Name = name;
            NewArtist.Id = id;

            // Checking duplicity
            var foundArtist = DatabaseUtilities.FindArtistNode(NewArtist.Name, NewArtist.Id, Artists);
            if (foundArtist != null)
            {
                return;
            }
            Artists.Add(NewArtist);

            
        }
        public void AddArtists(List<Artist> artists)
        {
            List<Artist> uniqueArtists = new List<Artist>();

            // Checking duplicity
            foreach (var artist in artists)
            {
                var foundArtist = DatabaseUtilities.FindArtistNode(artist.Name, artist.Id, Artists);
                if (foundArtist != null)
                {
                    continue;
                }
                uniqueArtists.Add(artist);
            }
            Artists.AddRange(uniqueArtists);           
        }

        public void RemoveArtist(Artist artist)
        {
            if (Artists.Contains(artist))
            {
                Artists.Remove(artist);
            }
            else
            {
                RemoveArtist(artist.Name, artist.Id);
            }
        }
        public void RemoveArtist(string name, string id)
        {
            Artist activeArtist = DatabaseUtilities.FindArtistNode(name, id, Artists);

            if (activeArtist != null)
            {
                Artists.Remove(activeArtist);
            }
        }

        public void AddAlbum(string albumName, string albumId, string artistName, string artistId)
        {
            Album NewAlbum = new Album();
            NewAlbum.Name = albumName;
            NewAlbum.Id = albumId;

            Artist activeArtist = DatabaseUtilities.FindArtistNode(artistName, artistId, Artists);

            if (activeArtist != null)
            {
                activeArtist.Albums.Add(NewAlbum);
            }
           
        }
        public void AddAlbums(List<Album> albums, string artistName, string artistId)
        {
            Artist activeArtist = DatabaseUtilities.FindArtistNode(artistName, artistId, Artists);

            if (activeArtist != null)
            {
                activeArtist.Albums.AddRange(albums);
            }
        }

        public void RemoveAlbum(string albumName, string albumId, string artistName, string artistId)
        {
            DatabaseUtilities.AlbumNode activeAlbum = DatabaseUtilities.FindAlbumNode(albumName, albumId, artistName, artistId, Artists);

            if (activeAlbum.albumNode != null)
            {
                activeAlbum.artistNode.Albums.Remove(activeAlbum.albumNode);
            }
        }

        public void TrackAlbums(List<Album> albums)
        {
            foreach(var album in albums)
            {
                album.Tracked = true;
            }
        }

        public void AddTracks(List<Track> tracks, string albumName, string albumId, string artistName, string artistId)
        {
            DatabaseUtilities.AlbumNode activeAlbum = DatabaseUtilities.FindAlbumNode(albumName, albumId, artistName, artistId, Artists);            

            activeAlbum.albumNode.Tracks.AddRange(tracks);               
        }
        public void RemoveTrack(string trackName, string albumName, string albumId, string artistName, string artistId)
        {
            DatabaseUtilities.TrackNode activeTrack = DatabaseUtilities.FindTrackNode(trackName, albumName, albumId, artistName, artistId, Artists);

            if (activeTrack.trackNode != null)
            {
                activeTrack.albumNode.Tracks.Remove(activeTrack.trackNode);
            }
        }

        public void ModifyDate(Track track, int uts)
        {
            track.LastListenedUTS = uts;
        }


        public List<Artist> Artists;
    }

   
    
}

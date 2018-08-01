using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog
{
    public class PlaylistInstance
    {
        private XmlHandler _xmlHandler;
        private List<PlaylistObject> _playlists;

        public PlaylistInstance()
        {
            _xmlHandler = new XmlHandler();
            _playlists = new List<PlaylistObject>();
        }

        public PlaylistInstance(string filePath)
        {
            _xmlHandler = new XmlHandler();
            Load(filePath);
        }

        public void Save(string fileName)
        {
            _xmlHandler.Serialize(_playlists, fileName);
        }
        public void Load(string filePath)
        {
            _playlists = new List<PlaylistObject>();
            _xmlHandler.Deserialize(_playlists, filePath);
        }

        public void Add(PlaylistObject playlist)
        {
            _playlists.Add(playlist);
        }
        public void Remove(PlaylistObject playlist)
        {
            _playlists.Remove(playlist);
        }

        public List<PlaylistObject> GetPlaylists()
        {
            return _playlists;
        }

        public List<PlaylistObject> GetPlaylists(string playlistName)
        {
            var playlistPackage = new List<PlaylistObject>();
            foreach (var playlist in _playlists)
            {
                if (playlist.Name == playlistName)
                {
                    playlistPackage.Add(playlist);
                }
            }
            return playlistPackage;
        }
    }
}

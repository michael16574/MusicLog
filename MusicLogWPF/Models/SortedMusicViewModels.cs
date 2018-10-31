using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLogWPF
{
    public class SortedMusicViewModels
    {
        private Dictionary<string, List<ITrackViewModel>> _artists;
        private Dictionary<string, List<ITrackViewModel>> _albums;
        private Dictionary<string, List<ITrackViewModel>> _tracks;
        private Dictionary<string, string> _existingArtistID;
        private Dictionary<string, string> _existingAlbumID;
        private Dictionary<string, string> _existingTrackID;

        public SortedMusicViewModels()
        {
            _artists = new Dictionary<string, List<ITrackViewModel>>();
            _albums = new Dictionary<string, List<ITrackViewModel>>();
            _tracks = new Dictionary<string, List<ITrackViewModel>>();
            _existingArtistID = new Dictionary<string, string>();
            _existingAlbumID = new Dictionary<string, string>();
            _existingTrackID = new Dictionary<string, string>();

        }

        public void Add(IArtistViewModel artist)
        {
            if (_existingArtistID.ContainsKey(artist.ID))
            {
                return;
            }
            
            _artists.Add(artist.Name.ToLower(), new List<ITrackViewModel>());
            _existingArtistID.Add(artist.ID, artist.Name.ToLower());
        }

        public void Add(IAlbumViewModel album)
        {
            if (_existingAlbumID.ContainsKey(album.ID))
            {
                return;
            }

            _albums.Add(album.Name.ToLower(), new List<ITrackViewModel>());
            _existingAlbumID.Add(album.ID, album.Name.ToLower());
        }

        public void Add(ITrackViewModel track)
        {
            if (_existingTrackID.ContainsKey(track.ID))
            {
                return;
            }
            
            if (!_tracks.ContainsKey(track.Name.ToLower()))
            {
                _tracks.Add(track.Name.ToLower(), new List<ITrackViewModel>());
            }

            _tracks[track.Name.ToLower()].Add(track);

            string albumName = _existingAlbumID[track.AlbumID];
            _albums[albumName].Add(track);

            string artistName = _existingArtistID[track.ArtistID];
            _artists[artistName].Add(track);

            
        }

        public List<ITrackViewModel> Find(string query)
        {
            // Utilizes dictionary search capabilities
            query = query.ToLower();
            var package = new HashSet<ITrackViewModel>();
            
            if (_artists.ContainsKey(query))
            {
                var artistQuery = _artists[query];
                foreach (var track in artistQuery)
                {
                    package.Add(track);
                }
            }

            if (_albums.ContainsKey(query))
            {
                var albumQuery = _albums[query];
                foreach (var track in albumQuery)
                {
                    package.Add(track);
                }
            }

            if (_tracks.ContainsKey(query))
            {
                var trackQuery = _tracks[query];
                foreach (var track in trackQuery)
                {
                    package.Add(track);
                }
            }

            return package.ToList();
        }

        public List<ITrackViewModel> FindSimilar(string query)
        {
            // Sacrifices dictionary search capabilities for StartsWith flexability
            query = query.ToLower();
            var package = new HashSet<ITrackViewModel>();

            var artistQuery = _artists.Where(a => a.Key.StartsWith(query)).SelectMany(a => a.Value);
            var albumQuery = _albums.Where(a => a.Key.StartsWith(query)).SelectMany(a => a.Value);
            var trackQuery = _tracks.Where(a => a.Key.StartsWith(query)).SelectMany(a => a.Value);

            foreach (var track in artistQuery)
            {
                package.Add(track);
            }
            foreach (var track in albumQuery)
            {
                package.Add(track);
            }
            foreach (var track in trackQuery)
            {
                package.Add(track);
            }

            return package.ToList();
        }

    }
}

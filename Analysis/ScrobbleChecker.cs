using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog.Analysis
{
    public static class ScrobbleChecker
    {
        public static void UpdateAllArtists(Database.DatabaseInstance database)
        {
            foreach(var artist in database.Artists)
            {
                UpdateArtist(artist, database);
            }
        }

        public static void UpdateArtist(Database.Artist artist, Database.DatabaseInstance database)
        {
            var trackedAlbums = artist.Albums.Where(a => a.Tracked == true).ToList();
            if (trackedAlbums.Count == 0)
            {
                return;
            }

            List<LastFM.LastFMResponse_ArtistTracks.Track> tracksNonUnique = LastFM.LastFMUtilities.GetUserTracks("eriejar", artist.Name);
            var tracks = tracksNonUnique.GroupBy(t => new { t.name, t.album.text })
                                        .Select(t => t.First())
                                        .ToList();

            foreach (var lfmTrack in tracks)
            {
                // Match correct album
                var matchedAlbum = trackedAlbums.Find(a => a.Name == lfmTrack.album.text);               
                
                if (matchedAlbum == null)
                {
                    continue;
                }

                // Find correct song to modify uts
                var songList = matchedAlbum.Tracks;
                foreach (Database.Track dbTrack in songList)
                {
                    if (lfmTrack.name.ToLower() != dbTrack.Name.ToLower())
                    {
                        continue;
                    }
                    else
                    {
                        dbTrack.LastListenedUTS = Int32.Parse(lfmTrack.date.uts);
                    }
                }
            }
        }



    }
}

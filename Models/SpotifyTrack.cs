﻿using System;

namespace MusicLog
{
    public class SpotifyTrack : ISpotifyMusicObject, ITrack
    {
        public string Name { get; set; }
        public string SpotifyID { get; set; }
        public Guid ID { get; set; }

        public Guid ArtistID { get; set; }
        public Guid AlbumID { get; set; }
        public int TrackNo { get; set; }
        public int LastListenedUnix { get; set; }

        public SpotifyTrack()
        {
            ArtistID = Guid.Empty;
            AlbumID = Guid.Empty;
            ID = Guid.NewGuid();
        }
    }
}
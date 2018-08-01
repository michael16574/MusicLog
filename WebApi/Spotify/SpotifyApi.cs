using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace MusicLog
{
    public static class SpotifyApi
    {
        private static ClientCredentialsAuth _auth;
        private static SpotifyWebAPI _authobj;


        public static SpotifyWebAPI GetAuthObj(string clientID, string clientSecret)
        {
            // Generates an object that can be used to authorize any requests to the Spotify API
            _auth = new ClientCredentialsAuth()
            {

                ClientId = clientID,
                ClientSecret = clientSecret,
                Scope = Scope.UserReadPrivate
            };

            Token token = _auth.DoAuth();
            _authobj = new SpotifyWebAPI()
            {
                TokenType = token.TokenType,
                AccessToken = token.AccessToken,
                UseAuth = true
            };

            return _authobj;          
        }

        public static List<FullArtist> SearchArtists(string query, SpotifyWebAPI authObj)
        {          
            SearchItem searchItems = authObj.SearchItems(query, SearchType.Artist, 10);
            return searchItems.Artists.Items;
        }

        public static List<SimpleAlbum> SearchAlbums(Artist artistObj, SpotifyWebAPI authObj)
        {
            Paging<SimpleAlbum> PagingObj = authObj.GetArtistsAlbums(artistObj.SpotifyID, AlbumType.Album);
            List<SimpleAlbum> SimpleAlbumList = new List<SimpleAlbum>();     
            foreach (SimpleAlbum album in PagingObj.Items)
            {
                SimpleAlbumList.Add(album);
            }
            return SimpleAlbumList;
        }

        public static List<SimpleTrack> SearchTracks(Album albumObj, SpotifyWebAPI authObj)
        {
            Paging<SimpleTrack> PagingObj = authObj.GetAlbumTracks(albumObj.SpotifyID, 50);
            List<SimpleTrack> SimpleTrackList = new List<SimpleTrack>();
            foreach (SimpleTrack track in PagingObj.Items)
            {
                SimpleTrackList.Add(track);
            }
            return SimpleTrackList;
        }

        public static FullPlaylist CreatePlaylist(string playlistName, string userID, SpotifyWebAPI authObj)
        {
            FullPlaylist spotifyPlaylist = authObj.CreatePlaylist(userID, playlistName);
            return spotifyPlaylist;
        }

        public static void AddPlaylistTracks(List<string> trackUris, FullPlaylist spotifyPL, SpotifyWebAPI authObj)
        {
            authObj.AddPlaylistTracks(spotifyPL.Owner.Id, spotifyPL.Id, trackUris);
        }

        
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace MusicLog
{
    public static class SpotifyApi
    {
        private static SpotifyWebAPI _authobj;


        public static async Task<SpotifyWebAPI> GetAuthObjByImplicitGrant(string clientID)
        {
            WebAPIFactory webApiFactory = new WebAPIFactory("http://localhost",
                                                            8000,
                                                            clientID,
                                                            Scope.UserReadPrivate | Scope.PlaylistModifyPrivate | Scope.PlaylistModifyPublic,
                                                            TimeSpan.FromSeconds(20));

            try
            {
                _authobj = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (_authobj == null)
            {
                return null;
            }

            return _authobj;
        }

        
        public static SpotifyWebAPI GetAuthObjByClientCred(string clientID, string clientSecret)
        {
            var auth = new ClientCredentialsAuth()
            {

                ClientId = clientID,
                ClientSecret = clientSecret,
                Scope = Scope.UserReadPrivate | Scope.PlaylistModifyPrivate | Scope.PlaylistModifyPublic
            };

            Token token = auth.DoAuth();
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

        public static List<SimpleAlbum> SearchAlbums(SpotifyArtist artistObj, SpotifyWebAPI authObj)
        {
            Paging<SimpleAlbum> PagingObj = authObj.GetArtistsAlbums(artistObj.SpotifyID, AlbumType.Album);
            List<SimpleAlbum> SimpleAlbumList = new List<SimpleAlbum>();     
            foreach (SimpleAlbum album in PagingObj.Items)
            {
                SimpleAlbumList.Add(album);
            }
            return SimpleAlbumList;
        }

        public static List<SimpleTrack> SearchTracks(SpotifyAlbum albumObj, SpotifyWebAPI authObj)
        {
            Paging<SimpleTrack> PagingObj = authObj.GetAlbumTracks(albumObj.SpotifyID, 50);
            List<SimpleTrack> SimpleTrackList = new List<SimpleTrack>();
            foreach (SimpleTrack track in PagingObj.Items)
            {
                SimpleTrackList.Add(track);
            }
            return SimpleTrackList;
        }

        public static List<FullTrack> SearchTracksByQuery(string query, SpotifyWebAPI authObj)
        {   
            List<FullTrack> tracks = authObj.SearchItems(query, SearchType.Track).Tracks.Items;
            return tracks;
        }

        public static FullPlaylist CreatePlaylist(string playlistName, string userID, SpotifyWebAPI authObj)
        {
            FullPlaylist spotifyPlaylist = authObj.CreatePlaylist(userID, playlistName);
            return spotifyPlaylist;
        }

        public static void AddPlaylistTracks(List<string> trackUris, FullPlaylist spotifyPL, SpotifyWebAPI authObj)
        {
            var fullTrackUris = new List<string>();
            int counter = 0;

            foreach (string uri in trackUris)
            {
                fullTrackUris.Add("spotify:track:" + uri);
                counter += 1;

                if (counter >= 100)
                {                   
                    authObj.AddPlaylistTracks(spotifyPL.Owner.Id, spotifyPL.Id, fullTrackUris);
                    counter = 0;
                    fullTrackUris.Clear();
                }                
            }

            authObj.AddPlaylistTracks(spotifyPL.Owner.Id, spotifyPL.Id, fullTrackUris);
        }

        
    }
}


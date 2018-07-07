using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace MusicLog.Spotify
{
    public static class SpotifyUtilities
    {
        public static SpotifyWebAPI GetAuthObj()
        {
            // Generates an object that can be used to authorize any requests to the Spotify API
            string[] clientinfo = getClientInfo();
            _auth = new ClientCredentialsAuth()
            {

                ClientId = clientinfo[0],
                ClientSecret = clientinfo[1],
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

        private static ClientCredentialsAuth _auth;
        private static string[] getClientInfo()
        {
            string[] lines = System.IO.File.ReadAllLines("clientinfo.txt");
            return lines;
        }

        public static List<Database.Artist> GetArtists(string query, SpotifyWebAPI AuthObj)
        {
            List<FullArtist> items = SearchArtists(query, AuthObj).Artists.Items;
            List<Database.Artist> artistList = new List<Database.Artist>();
            foreach (FullArtist artist in items)
            {
                Database.Artist newArtist = new Database.Artist
                {
                    Name = artist.Name,
                    Id = artist.Id
                };
                artistList.Add(newArtist);
            }
            return artistList;
        } 

        private static SearchItem SearchArtists(string query, SpotifyWebAPI AuthObj)
        {          
            SearchItem items = AuthObj.SearchItems(query, SearchType.Artist, 10);
            return items;
        }

        public static List<Database.Album> GetAlbums(Database.Artist ArtistObj, SpotifyWebAPI AuthObj)
        {
            List<SimpleAlbum> SimpleAlbumList = SearchAlbums(ArtistObj, AuthObj);
            List<Database.Album> AlbumList = new List<Database.Album>();
            foreach (SimpleAlbum album in SimpleAlbumList)
            {
                Database.Album newAlbum = new Database.Album
                {
                    Name = album.Name,
                    Id = album.Id
                };
                AlbumList.Add(newAlbum);
            }
            return DeleteDuplicateAlbums(AlbumList);
        }

        private static List<SimpleAlbum> SearchAlbums(Database.Artist ArtistObj, SpotifyWebAPI AuthObj)
        {
            Paging<SimpleAlbum> PagingObj = AuthObj.GetArtistsAlbums(ArtistObj.Id, AlbumType.Album);
            List<SimpleAlbum> SimpleAlbumList = new List<SimpleAlbum>();     
            foreach (SimpleAlbum album in PagingObj.Items)
            {
                SimpleAlbumList.Add(album);
            }
            return SimpleAlbumList;
        }

        private static List<Database.Album> DeleteDuplicateAlbums(List<Database.Album> albums)
        {
            List<Database.Album> albumRemovalList = new List<Database.Album>();
            for (int i = 1; i < albums.Count(); i++)
            { 
                for (int j = 0; j < i; j++)
                {
                    if (albums[i].Name == albums[j].Name)
                    {
                        if (albums[i].Tracks.Count() >= albums[j].Tracks.Count())
                        {
                            albumRemovalList.Add(albums[j]);
                        }
                        else
                        {
                            albumRemovalList.Add(albums[i]);
                        }
                    }
                }               
            }

            foreach(var album in albumRemovalList)
            {
                albums.Remove(album);
            }

            return albums;
        }

        public static List<Database.Track> GetTracks(Database.Album AlbumObj, SpotifyWebAPI AuthObj)
        {
            List<SimpleTrack> SimpleTrackList = SearchTracks(AlbumObj, AuthObj);
            List<Database.Track> TrackList = new List<Database.Track>();
            foreach (SimpleTrack track in SimpleTrackList)
            {
                Database.Track NewTrack = new Database.Track(track.Name);
                NewTrack.TrackNo = track.TrackNumber;
                TrackList.Add(NewTrack);
            }
            return TrackList;
        }

        private static List<SimpleTrack> SearchTracks(Database.Album AlbumObj, SpotifyWebAPI AuthObj)
        {
            Paging<SimpleTrack> PagingObj = AuthObj.GetAlbumTracks(AlbumObj.Id, 50);
            List<SimpleTrack> SimpleTrackList = new List<SimpleTrack>();
            foreach (SimpleTrack track in PagingObj.Items)
            {
                SimpleTrackList.Add(track);
            }
            return SimpleTrackList;
        }

        private static SpotifyWebAPI _authobj;
        
    }
}


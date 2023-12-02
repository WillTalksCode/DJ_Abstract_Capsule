using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DJ_Abstract_Capsule.Objects
{
    public static class Utility
    {

        public static DateTime SetDefaultDate(this DateTime? date)
        {
            if (date == null) { return new DateTime(1900, 1, 1); }
            else
                return Convert.ToDateTime(date);
        }

        public static string GetCommaSeparatedString(this List<string> list)
        {
            string clist = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                clist += list[i];
                if (i + 1 < list.Count)
                    clist += ",";
            }
            return clist;
        }
        public static List<Album> FillTheCrates()
        {
            List<Album> albums = new List<Album>();
            JObject albumsJSON = JObject.Parse(File.ReadAllText(@"Resources\Albums.json"));
            if (albumsJSON != null && albumsJSON["albums"] != null)
            {
                IList<JToken> albumsListJSON = albumsJSON["albums"].Children().ToList();
                if (albumsListJSON != null && albumsListJSON.Any())
                {
                    foreach (JToken albumToken in albumsListJSON)
                    {
                        Album album = new Album();

                        //Get Artists (Usually there is just one)
                        if (albumToken["artists"] != null)
                        {
                            IList<JToken> artistList = albumToken["artists"].Children().ToList();
                            if (artistList != null && artistList.Any())
                            {
                                for (int i = 0; i < artistList.Count; i++)
                                {
                                    JToken artistInfo = artistList[i];
                                    album.Artists.Add(artistInfo["name"] != null ? artistInfo["name"].ToString() : string.Empty);
                                }

                            }
                        }
                        album.AlbumName = albumToken["name"] != null ? albumToken["name"].ToString() : string.Empty;
                        DateTime result;
                        album.ReleaseDate = albumToken["release_date"] != null && DateTime.TryParse(albumToken["release_date"].ToString(), out result) ? Convert.ToDateTime(albumToken["release_date"]) : null;

                        //Get Genres
                        if (albumToken["genres"] != null)
                        {
                            JToken genreData = albumToken["genres"];
                            album.Genres = JsonConvert.DeserializeObject<List<string>>(genreData.ToString());
                            //album.Genres = albumToken.Select(x => x.Value<string>("genres")).ToList();
                            //JArray genreArray = new JArray(albumToken["genres"]);
                            //album.Genres.Select(x => x.Value<string>()).to
                            //album.Genres = genreArray.ToString().Split(',').ToList();
                        }

                        //Get Tracks and set explicit content since it's not in the restriction node for some reason.
                        if (albumToken["tracks"] != null)
                        {
                            IList<JToken> trackList = albumToken["tracks"]["items"].Children().ToList();
                            if (trackList != null && trackList.Any())
                            {
                                foreach (JToken trackInfo in trackList)
                                {
                                    List<string> trackArtists = new List<string>();
                                    //Get track artist
                                    if (trackInfo["artists"] != null)
                                    {
                                        IList<JToken> trackArtistList = trackInfo["artists"].Children().ToList();
                                        if (trackArtistList != null && trackArtistList.Any())
                                        {
                                            for (int i = 0; i < trackArtistList.Count; i++)
                                            {
                                                JToken trackArtistInfo = trackArtistList[i];
                                                trackArtists.Add(trackArtistInfo["name"] != null ? trackArtistInfo["name"].ToString() : string.Empty);
                                            }

                                        }
                                    }



                                    bool explicitTrack = (trackInfo["explicit"] != null && (bool)trackInfo["explicit"]);
                                    if (explicitTrack)
                                    {
                                        album.ExplicitContent = true;
                                    }
                                    
                                    string SongName = trackInfo["name"] != null ? trackInfo["name"].ToString() : string.Empty;

                                    album.Songs.Add(new Song(SongName, explicitTrack, trackArtists));
                                }

                            }
                        }
                        albums.Add(album);
                    }

                }
            }


            return albums;

        }
    }
}

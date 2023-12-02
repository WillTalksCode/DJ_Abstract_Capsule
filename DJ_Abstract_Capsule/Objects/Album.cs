using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJ_Abstract_Capsule.Objects;

namespace DJ_Abstract_Capsule.Objects
{
    public class Album
    {
        public List<string> Artists { get; set; }
        public string AlbumName { get; set; }
        public DateTime? ReleaseDate
        {
            get { return releaseDate; }
            set
            {
                releaseDate = value.SetDefaultDate();
            }
        }
        private DateTime releaseDate;
        public List<Song> Songs { get; set; }
        public List<string> Genres { get; set; }
        public bool ExplicitContent { get; set; }

        public Album()
        {
            Artists = new List<string>();
            AlbumName = string.Empty;
            ReleaseDate = new DateTime(1900, 1, 1);
            Songs = new List<Song>();
            Genres = new List<string>();
            ExplicitContent = false;
        }

        public Album(List<string> artists, string albumName, DateTime? releaseDate, List<Song> songs, List<string> genres, bool explicitContent)
        {
            Artists = artists;
            AlbumName = albumName;
            ReleaseDate = releaseDate;
            Songs = songs;
            Genres = genres;
            ExplicitContent = explicitContent;
        }

        /*public Song PickASong(string song = "")
        {
            if (Songs.Count > 0)
            {
                return "There are no songs here!";
            }
            Random random = new Random();
            var Request = string.Empty;
            if (string.IsNullOrEmpty(song)) { return Songs[random.Next(Songs.Count)]; }
            else
            {
                Request = (from x in Songs where x.ToLowerInvariant().Contains(song) select x).FirstOrDefault();
                if (string.IsNullOrEmpty(Request))
                    return "Sorry, don't got it";
                else
                    return Request;
            }

        }*/

        public int GetYear()
        {
            return releaseDate.Year;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DJ_Abstract_Capsule.Objects
{
    public class DJ
    {
        public string DJName { get; set; }
        public double HourlyRate { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        List<Album> Crate;
        public List<string> SetList { get; set; }

        public DJ(bool FillCrates = false)
        {
            Crate = FillCrates ? Utility.FillTheCrates() : new List<Album>();
            DJName = string.Empty;
            HourlyRate = 0;
            ContactEmail = string.Empty;
            ContactPhone = string.Empty;

        }

        public DJ(string dJName, double hourlyRate, string contactEmail, string contactPhone, List<Album> crate)
        {
            DJName = dJName;
            HourlyRate = hourlyRate;
            ContactEmail = contactEmail;
            ContactPhone = contactPhone;
            Crate = crate;
        }


        private List<Album> Get80sMusic()
        {
            return Crate.Where(x => (x.GetYear() >= 1980 && x.GetYear() <= 1989) || x.Genres.Contains("80s")).ToList();
        }

        private List<Album> Get90sMusic()
        {
            return Crate.Where(x => (x.GetYear() >= 1990 && x.GetYear() <= 1999) || x.Genres.Contains("90s")).ToList();
        }

        private List<Album> Get70sMusic()
        {
            return Crate.Where(x => (x.GetYear() >= 1970 && x.GetYear() <= 1979) || x.Genres.Contains("70s")).ToList();
        }

        private List<Album> Get2000sMusic()
        {
            return Crate.Where(x => x.GetYear() >= 2000 && x.GetYear() <= 2009).ToList();
        }

        private List<Album> GetModernMusic()
        {
            return Crate.Where(x => x.GetYear() >= 2010).ToList();
        }

        private List<Album> GetOldSchool()
        {
            List<string> classicGenres = new List<string>() { "60s", "70s", "80s", "90s" };
            return Crate.Where(x => (x.GetYear() >= 1950 && x.GetYear() <= 2000) || x.Genres.Intersect(classicGenres).Any()).ToList();
        }


        private List<Album> FilterSetListByGenre(List<string> genres)
        {

            return (from x in Crate where containsAny(x.Genres, genres) select x).ToList();
        }

        private bool containsAny(List<string> A, List<string> B)
        {
            return A.Any(x => B.Contains(x));
        }

        private List<Album> NoProfanity()
        {
            return (from x in Crate where x.ExplicitContent == false select x).ToList();
        }



        public List<Song> BuildSetList(int SetLength, List<string> genres, bool noProfanity = false, int decadeFilter = 0)
        {
            List<Album> list = new List<Album>();
            List<Song> setSongs = new List<Song>();
            if (genres.Any())
                list = FilterSetListByGenre(genres);
            else
                list = Crate;

            switch (decadeFilter)
            {
                case 1: //70s
                    list = Get70sMusic();
                    break;
                case 2:
                    list = Get80sMusic();
                    break;
                case 3:
                    list = Get90sMusic();
                    break;
                case 4:
                    list = GetOldSchool();
                    break;
                case 5:
                    list = Get2000sMusic();
                    break;
                case 6:
                    list = GetModernMusic();
                    break;
                default: break;

            }

            foreach (Album album in list)
            {
                if (noProfanity)
                    setSongs.AddRange(album.Songs);
                else
                    setSongs.AddRange(from x in album.Songs where x.ExplicitContent == false select x);
            }

            List<Song> Set = new List<Song>();
            while (Set.Count < SetLength)
            {
                if (setSongs.Count == 0)
                    break;
                Random random = new Random();

                Song currentSong = setSongs[random.Next(setSongs.Count)];
                setSongs.RemoveAt(random.Next(setSongs.Count));
                Set.Add(currentSong);

            }

            return Set;

        }

        public string GetCrateList()
        {
            string ListOfCrates = string.Empty;
            foreach (Album album in Crate)
            {
                ListOfCrates += "Artist: " + album.Artists[0] + " Title: " + album.AlbumName + " Release Year: " + album.GetYear() + Environment.NewLine;
                if (album.ExplicitContent)
                    ListOfCrates += "*WARNING!!!  Explicit Content!" + Environment.NewLine;
                int trackNumber = 1;
                foreach (Song track in album.Songs)
                {
                    ListOfCrates += trackNumber + "." + track.Title;
                    if (track.ExplicitContent)
                        ListOfCrates += "*" + Environment.NewLine;
                    else
                        ListOfCrates += Environment.NewLine;
                    trackNumber++;
                }
                ListOfCrates += "___________________________________________" + Environment.NewLine;
                ListOfCrates += "" + Environment.NewLine;

            }
            return ListOfCrates;
        }


    }
}

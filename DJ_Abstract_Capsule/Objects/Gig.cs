using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJ_Abstract_Capsule.Objects
{
    public class Gig
    {
        public DJ TheDJ { get; set; }
        public List<Song> SongList { get; set; }

        public Gig(DJ theDJ, SetRequest request)
        {
            TheDJ = theDJ;
            SongList = TheDJ.BuildSetList(request.SetLength, request.Genres, request.NoProfanity, request.DecadeFilter);
        }
    }
}

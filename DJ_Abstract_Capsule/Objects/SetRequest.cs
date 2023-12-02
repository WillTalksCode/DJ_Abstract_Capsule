using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJ_Abstract_Capsule.Objects
{
    public struct SetRequest
    {
        public int SetLength { get; set; }
        public List<String> Genres { get; set; }
        public bool NoProfanity { get; set; }
        public int DecadeFilter { get; set; }

        public SetRequest()
        {
            SetLength = 0;
            Genres = new List<String>();
            NoProfanity = false;
            DecadeFilter = 0;
        }

        public SetRequest(int setLength, List<string> genres, bool noProfanity, int decadeFilter)
        {
            SetLength = setLength;
            Genres = genres;
            this.NoProfanity = noProfanity;
            DecadeFilter = decadeFilter;
        }

    }
}

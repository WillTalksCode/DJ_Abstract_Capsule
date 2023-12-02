using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DJ_Abstract_Capsule.Objects
{
    public class Song
    {
        public string Title { get; set; }
        public bool ExplicitContent { get; set; }
        public List<string> Artists { get; set; }

        public Song()
        {
            Title = string.Empty;
            ExplicitContent = false;
            Artists = new List<string>();
        }

        public Song(string title, bool explicitContent, List<string> artists)
        {
            Title = title;
            ExplicitContent = explicitContent;
            Artists = artists;

        }
    }
}

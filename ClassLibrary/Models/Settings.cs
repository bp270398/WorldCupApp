using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Settings
    {
        private const char DEL = '|';
        public static string language;
        public static bool gender;
        public static string resolution;
        public static string homeTeamCountry;
        public static string awayTeamCountry;
        public static int homeTeamCountryIndex;
        public static int awayTeamCountryIndex;
        public static HashSet<string> favourites;

        public override string ToString()
            => $"{language}{DEL}{gender}{DEL}{resolution}{DEL}{homeTeamCountry}{DEL}{awayTeamCountry}{DEL}{homeTeamCountryIndex}{DEL}{awayTeamCountryIndex}";
    }
}

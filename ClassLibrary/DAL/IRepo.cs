using ClassLibrary.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace ClassLibrary.DAL
{
    public interface IRepo
    {
        bool GetExistingPath();
        void CreateSettings();
        void SaveSettings();
        List<string> LoadSettings();
        void SaveFavourites(HashSet<string> favourites);
        HashSet<string> LoadFavourites();
        void SetCulture(string language);
        void LoadLanguage();
        Image GetDefaultImage();
        Task<HashSet<Teams>> LoadTeams();
        Task<HashSet<Match>> LoadMatches();
        Task<HashSet<Result>> LoadResults();
        string GetDefaultPath();
        string GetDefaultImagePath();
    }
}

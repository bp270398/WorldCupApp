using ClassLibrary.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary.DAL
{
    internal class Repository : IRepo
    {

        //  Web API links
        public const string MensResultsWebPath = "https://worldcup-vua.nullbit.hr/men/teams/results";
        public const string MensMatchesWebPath = "https://worldcup-vua.nullbit.hr/men/matches";
        public const string MensDetailedMatchesWebPath = "https://worldcup-vua.nullbit.hr/men/matches/country?fifa_code=ENG";
        public const string WomensResultsWebPath = "https://worldcup-vua.nullbit.hr/women/teams/results";
        public const string WomensMatchesWebPath = "https://worldcup-vua.nullbit.hr/women/matches";
        public const string WomensDetailedMatchesWebPath = "https://worldcup-vua.nullbit.hr/women/matches/country?fifa_code=ENG";
        /*
        public const string MensResultsWebPath = "https://world-cup-json-2018.herokuapp.com/teams/results";
        public const string MensMatchesWebPath = "https://world-cup-json-2018.herokuapp.com/matches";
        public const string MensDetailedMatchesWebPath = "https://world-cup-json-2018.herokuapp.com/matches/country?fifa_code=ENG";
        public const string WomensResultsWebPath = "https://worldcup.sfg.io/teams/results";
        public const string WomensMatchesWebPath = "https://worldcup.sfg.io/matches";
        public const string WomensDetailedMatchesWebPath = "https://worldcup.sfg.io/matches/country?fifa_code=ENG";
         
         */

        //  File sources
        public static string MenGroupResultsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "worldcup.sfg.io/men/group_results.json");
        public static string MenMatchesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "worldcup.sfg.io/men/matches.json");
        public static string MenResultsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "worldcup.sfg.io/men/results.json");
        public static string MenTeamsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "worldcup.sfg.io/men/teams.json");
        public static string WomenGroupResultsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "worldcup.sfg.io/women/group_results.json");
        public static string WomenMatchesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "worldcup.sfg.io/women/matches.json");
        public static string WomenResultsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "worldcup.sfg.io/women/results.json");
        public static string WomenTeamsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "worldcup.sfg.io/women/teams.json");

        private bool existingPath;

        private const char DEL = '|';
        public const string HR = "hr", EN = "en";
        public static string DEFAULT_PATH = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public static string SETTINGS_PATH = Path.Combine(DEFAULT_PATH, "Settings/settings.txt");
        public static string FAVOURITES_PATH = Path.Combine(DEFAULT_PATH, "Settings/favourites.txt");
        private string DEFAULT_SETTINGS = $"Hrvatski{DEL}True{DEL}720p{DEL} {DEL} {DEL}0{DEL}0{DEL}";

        public Repository()
        {
            if (!File.Exists(SETTINGS_PATH) || !File.Exists(FAVOURITES_PATH))
            {
                CreateSettings();
                existingPath = false;
            }
            else
            {
                existingPath = true;
            }
        }

        public bool GetExistingPath() => existingPath;
        public string GetDefaultPath() => DEFAULT_PATH;

        public void CreateSettings()
        {
            /*
             * 
            if (!File.Exists(SETTINGS_PATH)) { 
                File.Create(SETTINGS_PATH).Close();
            }
            */
            File.WriteAllText(SETTINGS_PATH, DEFAULT_SETTINGS);
            File.Create(FAVOURITES_PATH).Close();
        }
        public void SaveSettings()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder
                .Append(Settings.language)
                .Append(DEL)
                .Append(Settings.gender)
                .Append(DEL)
                .Append(Settings.resolution)
                .Append(DEL)
                .Append(Settings.homeTeamCountry)
                .Append(DEL)
                .Append(Settings.awayTeamCountry)
                .Append(DEL)
                .Append(Settings.homeTeamCountryIndex)
                .Append(DEL)
                .Append(Settings.awayTeamCountryIndex);
            File.WriteAllText(SETTINGS_PATH, stringBuilder.ToString());
        }
        public List<string> LoadSettings()
        {
            string[] lines = File.ReadAllLines(SETTINGS_PATH);
            List<string> result = new List<string>();
            foreach (string line in lines)
            {
                string[] details = line.Split(DEL);
                result.Add(details[0]);
                result.Add(details[1]);
                result.Add(details[2]);
                result.Add(details[3]);
                result.Add(details[4]);
                result.Add(details[5]);
                result.Add(details[6]);

                Settings.language = details[0];
                Settings.gender = Convert.ToBoolean(details[1]);
                Settings.resolution = details[2];
                Settings.homeTeamCountry = details[3];
                Settings.awayTeamCountry = details[4];
                Settings.homeTeamCountryIndex = int.Parse(details[5]);
                Settings.awayTeamCountryIndex = int.Parse(details[6]);
            }
            return result;
        }

        public void SaveFavourites(HashSet<string> favourites)
        {
            StringBuilder stringBuilder = new StringBuilder();

            favourites.ToList().ForEach(fav => stringBuilder.AppendLine(fav));

            File.WriteAllText(FAVOURITES_PATH, stringBuilder.ToString());
        }
        public HashSet<string> LoadFavourites()
        {
            string[] lines = File.ReadAllLines(FAVOURITES_PATH);
            HashSet<string> result = new HashSet<string>();
            foreach (string line in lines)
            {
                result.Add(line);
            }
            Settings.favourites = result;
            return result;
        }

        public void SetCulture(string language)
        {
            var culture = new CultureInfo(language);

            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
        public void LoadLanguage()
        {
            if ("Hrvatski".Equals(Settings.language))
            {
                SetCulture(HR);
            }
            else
            {
                SetCulture(EN);
            }

        }

        public Image GetDefaultImage() => Resources.defaultUser;
        public string GetDefaultImagePath() => Path.Combine(GetDefaultPath(), $"Assets/defaultUser.png");

        //Loads countries
        public Task<HashSet<Teams>> LoadTeams()
            => LoadItem<Teams>( MenTeamsPath,  WomenTeamsPath,  MensResultsWebPath,  WomensResultsWebPath);
        //Loads players
        public Task<HashSet<Match>> LoadMatches()
            => LoadItem<Match>( MenMatchesPath,  WomenMatchesPath,  MensMatchesWebPath,  WomensMatchesWebPath);
        //Loads detailed countries
        public Task<HashSet<Result>> LoadResults()
            => LoadItem<Result>( MenResultsPath,  WomenResultsPath,  MensResultsWebPath,  WomensResultsWebPath);


        // Helper methods
        private static Task<HashSet<T>> LoadItem<T>(string menItemPath, string womenItemPath, string mensItemWebPath, string womensItemWebPath)
        {
            if (File.Exists(menItemPath) || File.Exists(womenItemPath))
            {
                //File load
                return Settings.gender
                    ? Task.Run(() =>
                    {
                        return LoadJson<T>(menItemPath);
                    })
                    : Task.Run(() =>
                    {
                        return LoadJson<T>(womenItemPath);
                    });
            }
            else
            {
                //Web load
                return Settings.gender
                    ? Task.Run(() =>
                    {
                        return LoadWeb<T>(mensItemWebPath);
                    })
                    : Task.Run(() =>
                    {
                        return LoadWeb<T>(womensItemWebPath);
                    });
            }
        }
        private static HashSet<T> LoadJson<T>(string item)
        {
            using (StreamReader reader = new StreamReader(item))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<HashSet<T>>(json);
            }
        }
        private static HashSet<T> LoadWeb<T>(string item)
        {
            var apiClient = new RestClient(item);
            var response = apiClient.Execute<HashSet<T>>(new RestRequest());
            return JsonConvert.DeserializeObject<HashSet<T>>(response.Content);
        }

    }

}

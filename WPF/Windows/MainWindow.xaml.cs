using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF.UserControls;
using WpfAnimatedGif;

namespace WPF.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRepo repo = RepoFactory.GetRepo();
        //Home team set
        HashSet<Result> results = new HashSet<Result>();
        //Away team set
        HashSet<Match> matches = new HashSet<Match>();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private async void Init()
        {
            SetResolution();
            repo.LoadSettings();

            results = await repo.LoadResults();
            matches = await repo.LoadMatches();

            FillData();

            ddlHomeTeamCountry.SelectedIndex = Settings.homeTeamCountryIndex;
            ddlAwayTeamCountry.SelectedIndex = Settings.awayTeamCountryIndex;
        }

        private void FillData()
        {
            try
            {
                foreach (var result in results)
                    ddlHomeTeamCountry.Items.Add(result);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void SetResolution()
        {
            switch (Settings.resolution)
            {
                case "600p":
                    Width = 800;
                    Height = 600;
                    break;
                case "720p":
                    Width = 1280;
                    Height = 720;
                    break;
                case "1080p":
                    Width = 1920;
                    Height = 1080;
                    break;
                case "Fullscreen":
                    WindowState = WindowState.Maximized;
                    break;
            }
        }

        private void DdlHomeTeamCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ddlAwayTeamCountry.Items.Clear();

            Settings.homeTeamCountry = ddlHomeTeamCountry.SelectedItem.ToString();
            Settings.homeTeamCountryIndex = ddlHomeTeamCountry.SelectedIndex;
            repo.SaveSettings();
            lblHomeTeamCountry.Content = Settings.homeTeamCountry;

            FillAwayTeamData();
            ddlAwayTeamCountry.SelectedIndex = 0;
        }

        private void DdlAwayTeamCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ddlAwayTeamCountry.SelectedItem == null)
            {
                lblAwayTeamCountry.Content = "No games played found";
                lblScore.Content = 0;
                return;
            }

            Settings.awayTeamCountry = ddlAwayTeamCountry.SelectedItem.ToString();
            Settings.awayTeamCountryIndex = ddlAwayTeamCountry.SelectedIndex;
            repo.SaveSettings();
            lblAwayTeamCountry.Content = Settings.awayTeamCountry;

            //load players for both after both are loaded
            LoadPlayers();

            foreach (var match in matches.Where(match => match.HomeTeamCountry == Settings.homeTeamCountry && match.AwayTeamCountry == Settings.awayTeamCountry))
                lblScore.Content = $"{match.HomeTeam.Goals} : {match.AwayTeam.Goals}";
        }

        private void LoadPlayers()
        {
            spHomeGoalie.Children.Clear();
            spHomeDefender.Children.Clear();
            spHomeMidfield.Children.Clear();
            spHomeForward.Children.Clear();

            spAwayGoalie.Children.Clear();
            spAwayDefender.Children.Clear();
            spAwayMidfield.Children.Clear();
            spAwayForward.Children.Clear();

            try
            {
                foreach (var item in matches.Where(item => item.HomeTeamCountry == Settings.homeTeamCountry && item.AwayTeamCountry == Settings.awayTeamCountry))
                {
                    LoadPlayerData(item.HomeTeamStatistics.StartingEleven, Settings.homeTeamCountry);
                    LoadPlayerData(item.AwayTeamStatistics.StartingEleven, Settings.awayTeamCountry);
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void LoadPlayerData(List<StartingEleven> startingEleven, string teamCountry)
        {
            foreach (var item in startingEleven)
            {
                int goals = 0;
                int yellowCards = 0;
                LoadPlayerStatistics(item.Name, ref goals, ref yellowCards);
                switch (item.Position)
                {
                    case Position.Defender:
                        _ = teamCountry == Settings.homeTeamCountry ? spHomeDefender.Children.Add(new PlayerControl(item, goals, yellowCards)) : spAwayDefender.Children.Add(new PlayerControl(item, goals, yellowCards));
                        break;
                    case Position.Forward:
                        _ = teamCountry == Settings.homeTeamCountry ? spHomeForward.Children.Add(new PlayerControl(item, goals, yellowCards)) : spAwayForward.Children.Add(new PlayerControl(item, goals, yellowCards));
                        break;
                    case Position.Midfield:
                        _ = teamCountry == Settings.homeTeamCountry ? spHomeMidfield.Children.Add(new PlayerControl(item, goals, yellowCards)) : spAwayMidfield.Children.Add(new PlayerControl(item, goals, yellowCards));
                        break;
                    case Position.Goalie:
                        _ = teamCountry == Settings.homeTeamCountry ? spHomeGoalie.Children.Add(new PlayerControl(item, goals, yellowCards)) : spAwayGoalie.Children.Add(new PlayerControl(item, goals, yellowCards));
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadPlayerStatistics(string name, ref int goals, ref int yellowCards)
        {
            // get data for selected match
            foreach (var match in matches.Where(match =>
                match.HomeTeamCountry == Settings.homeTeamCountry && match.AwayTeamCountry == Settings.awayTeamCountry))
            {
                StatCounter(name, ref goals, ref yellowCards, match.HomeTeamEvents);
                StatCounter(name, ref goals, ref yellowCards, match.AwayTeamEvents);
            }
        }

        private static void StatCounter(string name, ref int goals, ref int yellowCards, List<TeamEvent> teamEvents)
        {
            foreach (var item in teamEvents.Where(item => name == item.Player))
            {
                switch (item.TypeOfEvent)
                {
                    case "goal":
                        goals++;
                        break;
                    case "yellow-card":
                        yellowCards++;
                        break;
                }
            }
        }

        private void FillAwayTeamData()
        {
            try
            {
                foreach (var match in matches.Where(match => match.HomeTeamCountry == Settings.homeTeamCountry))
                    ddlAwayTeamCountry.Items.Add(match);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void BtnHomeTeamCountryDetails_Click(object sender, RoutedEventArgs e)
            => RunDetails(Settings.homeTeamCountry);

        private void BtnAwayTeamCountryDetails_Click(object sender, RoutedEventArgs e)
            => RunDetails(Settings.awayTeamCountry);

        private void RunDetails(string teamCountry) 
            => results.ToList().ForEach(item => 
                { if (item.Country == teamCountry) { RunAnimation(item); } });

        public void RunAnimation(Result item)
        {
            imgLoading.Visibility = Visibility.Visible;
            var image = new BitmapImage(new Uri(Path.Combine(repo.GetDefaultPath(), $"WPF/Assets/loading.gif")));
            ImageBehavior.SetAnimatedSource(imgLoading, image);

            Task.Factory.StartNew(() => Thread.Sleep(500))
                .ContinueWith((t) =>
                {
                    new DetailsWindow(item).Show();
                    imgLoading.Visibility = Visibility.Hidden;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow= new SettingsWindow();
            Hide();
            settingsWindow.ShowDialog();
            Close();
        }
    }
}

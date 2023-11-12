using ClassLibrary.Models;
using System.Windows;

namespace WPF.Windows
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        Result countryDetails = new Result();
        public DetailsWindow(Result result)
        {
            InitializeComponent();
            countryDetails = result;
            Init();
        }

        private void Init()
        {
            lblCountry.Content = countryDetails.Country;
            lblFifaCode.Content = countryDetails.FifaCode;
            lblGamesPlayed.Content = countryDetails.GamesPlayed;
            lblWins.Content = countryDetails.Wins;
            lblLoss.Content = countryDetails.Losses;
            lblDraws.Content = countryDetails.Draws;
            lblGoalsFor.Content = countryDetails.GoalsFor;
            lblGoalsAgainst.Content = countryDetails.GoalsAgainst;
            lblGoalsDiff.Content = countryDetails.GoalDifferential;
        }
    }
}

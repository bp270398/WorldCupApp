using ClassLibrary.Models;
using System.Windows.Forms;

namespace WindowsFormsApp.UserControls
{
    public partial class RankedStadiumControl : UserControl
    {
        public Match RankedStadium { get; }
        public RankedStadiumControl(Match rankedStadium)
        {
            InitializeComponent();
            RankedStadium = rankedStadium;
            LoadStadium(rankedStadium);
        }
        private void LoadStadium(Match rankedStadium)
        {
            lblLocation.Text = rankedStadium.Location;
            lblAttendance.Text = rankedStadium.Attendance.ToString();
            lblHomeTeam.Text = rankedStadium.HomeTeamCountry;
            lblAwayTeam.Text = rankedStadium.AwayTeamCountry;
        }
    }
}

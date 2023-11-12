using ClassLibrary.Models;
using System.Windows.Forms;

namespace WindowsFormsApp.UserControls
{
    public partial class RankedPlayerControl : UserControl
    {
        public TeamEvent RankedPlayer { get; }
        public StartingEleven Player { get; }
        public int Result { get; }
        public int YellowCards { get; }

        public RankedPlayerControl(StartingEleven playerItem, int result)
        {
            InitializeComponent();
            Player = playerItem;
            Result = result;
            LoadPlayer(playerItem);
        }

        private void LoadPlayer(StartingEleven player)
        {
            lblName.Text = player.Name;
            lblNumber.Text = player.ShirtNumber.ToString();
            lblPosition.Text = player.Position.ToString();
            lblResult.Text = Result.ToString();
            lblYellowCards.Text = YellowCards.ToString();
            pbPlayer.Image = Player.PlayerImage;
        }
    }
}

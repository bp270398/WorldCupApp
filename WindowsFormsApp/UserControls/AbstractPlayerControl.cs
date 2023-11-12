using DAL.DAO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp.UserControls
{
    public partial class AbstractPlayerControl : UserControl
    {
        private IRepo repo = RepoFactory.GetRepo();

        public StartingEleven Player { get; private set; }
        public string Name { get; set; }
        public AbstractPlayerControl(StartingEleven player)
        {
            InitializeComponent();
            Player = player;

            Name = player.Name;
            lblName.Text = player.Name;
            lblNumber.Text = player.ShirtNumber.ToString();
            lblPosition.Text = player.Position.ToString();
            pbPlayer.Image = repo.GetPicture();

            //LoadSavedPlayerImage();
            player.PlayerImage = pbPlayer.Image;
        }
    }
}

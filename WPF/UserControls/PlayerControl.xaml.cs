using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF.Windows;

namespace WPF.UserControls
{
    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        private readonly IRepo repo = RepoFactory.GetRepo();
        private readonly string DEFAULT_IMAGE_PATH = $"Assets/PlayerThumbnails/";

        public StartingEleven Player { get; set; }
        public int YellowCards { get; set; }
        public int Goals { get; set; }
        public System.Windows.Media.ImageSource source;

        public PlayerControl(StartingEleven player, int goals, int yellowCards)
        {
            InitializeComponent();
            Player = player;
            YellowCards = yellowCards;
            Goals = goals;
            LoadPlayerImage();
            lblPlayerName.Content = player.Name;
            gridPlayer.MouseLeftButtonDown += GridPlayer_MouseLeftButtonDown;

            if (Settings.resolution == "600p")
            {
                lblPlayerName.FontSize = 8;
            }
        }

        private void GridPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
           => new PlayerDetailsWindow(this, source).Show();

        private void LoadPlayerImage()
        {
            string[] imagesPath = Directory.GetFiles(Path.Combine(repo.GetDefaultPath(), DEFAULT_IMAGE_PATH));

            if (imagesPath.Count() == 0)
            {
                imgPlayer.Source = new BitmapImage(new Uri(repo.GetDefaultImagePath(), UriKind.Absolute));
            }
            else
            {
                foreach (string imagePath in imagesPath)
                {
                    var imageName = $"{imagePath.Substring(imagePath.IndexOf(DEFAULT_IMAGE_PATH) + DEFAULT_IMAGE_PATH.Length)}";
                    if (Player.Name == imageName.Remove(imageName.IndexOf('.')))
                    {
                        imgPlayer.Source = new BitmapImage(new Uri(imagePath));
                        source = imgPlayer.Source;
                        return;
                    }
                    else
                    {
                        imgPlayer.Source = new BitmapImage(new Uri(repo.GetDefaultImagePath(), UriKind.Absolute));
                    }
                }
                source = imgPlayer.Source;
            }

        }
    }
}

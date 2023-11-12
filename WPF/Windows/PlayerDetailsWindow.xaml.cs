using ClassLibrary.DAL;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF.UserControls;
using WpfAnimatedGif;

namespace WPF.Windows
{
    /// <summary>
    /// Interaction logic for PlayerDetailsWindow.xaml
    /// </summary>
    public partial class PlayerDetailsWindow : Window
    {
        readonly IRepo repo = RepoFactory.GetRepo();
        public PlayerControl PlayerControl { get; }
        public ImageSource Source { get; }
     
        public PlayerDetailsWindow(PlayerControl player, ImageSource source)
        {
            InitializeComponent();
            PlayerControl = player;
            Source = source;
            RunAnimation();
        }

        private void RunAnimation()
        {
            var image = new BitmapImage(new Uri(Path.Combine(repo.GetDefaultPath(), $"WPF/Assets/loading.gif")));
            ImageBehavior.SetAnimatedSource(imgLoading, image);
            Task.Factory.StartNew(() => Thread.Sleep(500))
                .ContinueWith((t) =>
                {
                    imgLoading.Visibility = Visibility.Hidden;
                    Init();
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void Init()
        {
            lblName.Visibility = Visibility.Visible;
            lblShirtNmbr.Visibility = Visibility.Visible;
            lblPosition.Visibility = Visibility.Visible;
            lblGoals.Visibility = Visibility.Visible;
            lblYellowCards.Visibility = Visibility.Visible;
            lblCaptain.Visibility = Visibility.Visible;
            imgPlayer.Visibility = Visibility.Visible;
            lbl1.Visibility = Visibility.Visible;
            lbl2.Visibility = Visibility.Visible;
            lbl3.Visibility = Visibility.Visible;
            lbl4.Visibility = Visibility.Visible;
            lbl5.Visibility = Visibility.Visible;

            lblName.Content = PlayerControl.Player.Name;
            lblShirtNmbr.Content = PlayerControl.Player.ShirtNumber;
            lblPosition.Content = PlayerControl.Player.Position;
            lblGoals.Content = PlayerControl.Goals;
            lblYellowCards.Content = PlayerControl.YellowCards;
            lblCaptain.Content = PlayerControl.Player.Captain ? "Captain" : "";

            imgPlayer.Source = Source;
        }
    }
}


using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Threading;
using System.Windows;

namespace WPF.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private bool flag; //prevents two on close pop-ups
        
        private readonly IRepo repo = RepoFactory.GetRepo();
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            // lang
            Settings.language = (bool)rbtnCroatian.IsChecked ? "Hrvatski" : "English";
            if (Settings.language.Equals("Hrvatski"))
            {
                repo.SetCulture("hr");
            }
            else
            {
                repo.SetCulture("en");
            }
            // gender
            Settings.gender = (bool)rbtnMale.IsChecked;
            // resolution
            if ((bool)rbtn800x600.IsChecked)
            {
                Settings.resolution = "600p";
            }
            else if ((bool)rbtn1280x720.IsChecked)
            {
                Settings.resolution = "720p";
            }
            else if ((bool)rbtn1920x1080.IsChecked)
            {
                Settings.resolution = "1080p";
            }
            else
            {
                Settings.resolution = "Fullscreen";
            }

            Settings.homeTeamCountry = " ";
            repo.SaveSettings();
            
            DefaultClose();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) => DefaultClose();

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!flag)
            {
                DefaultClose();
            }
        }

        private void DefaultClose()
        {
            flag = true;
            var mainWindow = new MainWindow();
            this.Hide();
            mainWindow.ShowDialog();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
            InitRadioButtonData();
            repo.LoadSettings();
            repo.LoadLanguage();
        }

        private void InitRadioButtonData()
        {
            if(Thread.CurrentThread.CurrentCulture.Name == "hr")
            {
                rbtnCroatian.IsChecked = true;
            }
            else
            {
                rbtnEnglish.IsChecked = true;
            }
            if (Settings.gender) //"Male" : "Female"
            {
                rbtnMale.IsChecked = true;
            }
            else
            {
                rbtnFemale.IsChecked= true;
            }
            switch (Settings.resolution)
            {
                case "600p":
                    rbtn800x600.IsChecked = true;
                    break;

                case "720p":
                    rbtn1280x720.IsChecked = true;
                    break;

                case "1080p":
                    rbtn1920x1080.IsChecked = true;
                    break;

                default:
                    rbtnFullscreen.IsChecked = true;
                    break;
            }
        }
    }
}

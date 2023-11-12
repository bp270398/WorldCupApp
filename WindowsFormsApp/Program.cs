using ClassLibrary.DAL;
using System;
using System.Windows.Forms;
using WindowsFormsApp.Forms;

namespace WindowsFormsApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            IRepo repo = RepoFactory.GetRepo();
            if (!repo.GetExistingPath())
            {
                Application.Run(new SettingsForm());
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}

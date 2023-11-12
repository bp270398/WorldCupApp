using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp.Forms
{
    public partial class SettingsForm : Form
    {
        private readonly IRepo repo = RepoFactory.GetRepo();

        public SettingsForm()
        {
            repo.LoadLanguage();
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            repo.LoadSettings();
            LoadLanguage();
            LoadGender();

        }

        private void LoadLanguage()
        {
            cbLanguage.DataSource = new List<string> { "Hrvatski", "English" }; 
            if (Thread.CurrentThread.CurrentCulture.Name.Equals("hr"))
            {
                cbLanguage.SelectedText = "Hrvatski";
            }
            if (Thread.CurrentThread.CurrentCulture.Name.Equals("en"))
            {
                cbLanguage.SelectedText = "English";
            }
        }
        private void LoadGender()
        {
            cbGender.Items.AddRange(new string[] { "M", "F" });
            cbGender.SelectedText = Settings.gender ? "M" : "F";
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            Settings.language = cbLanguage.SelectedItem.ToString();
            if (Settings.language.Equals("Hrvatski"))
            {
                repo.SetCulture("hr");
            }
            else
            {
                repo.SetCulture("en");
            }
            Settings.gender = cbGender.SelectedItem.ToString().Equals("M") ? true : false;
            repo.SaveSettings();
            

            BtnCancel_Click(this, null);

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            var mainForm = new MainForm();
            this.Visible = false;
            mainForm.ShowDialog();
            this.Close();
        }
    }
}

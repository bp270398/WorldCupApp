using ClassLibrary.DAL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp.Forms
{
    public partial class PictureForm : Form
    {
        private readonly IRepo repo = RepoFactory.GetRepo();
        //0 - no change
        //1 - new image
        private int changed = 0; 
        public PictureForm(PictureBox pb, string pName)
        {
            InitializeComponent();
            Init(pb, pName);
        }

        private void Init(PictureBox pb, string pName) { pictureBox.Image = pb.Image; lblPlayer.Text = pName; }

        private void BtnSelectImage_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Images|*.bmp;*.jpg;*.jfif;*.jpeg;*.png;|All files|*.*",
                InitialDirectory = Path.Combine(repo.GetDefaultPath(), "Images")
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                changed = 1;
                MainForm mainForm = new MainForm();
                mainForm.Refresh();
                pictureBox.ImageLocation = openFileDialog.FileName; 
            }
        }
        public Image GetImage() => pictureBox.Image;
        public int GetChanged() => changed;
    }
}

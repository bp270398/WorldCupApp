
using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.Forms;

namespace WindowsFormsApp.UserControls
{
    public partial class PlayerControl : UserControl
    {
        private readonly IRepo repo = RepoFactory.GetRepo();
        private readonly string DEFAULT_IMAGE_PATH = $"Assets/PlayerThumbnails/";

        public StartingEleven Player { get; set; }
        public string PName { get; set; }
        public bool selected = false;

        public PlayerControl(StartingEleven player) 
        {
            InitializeComponent();
            Player = player;
            ContextMenuStrip = contextMenuStrip;
            LoadPlayer(player);
        }
        private void LoadPlayer(StartingEleven player)
        {
            PName = player.Name;
            lblName.Text = player.Name;
            lblNumber.Text = player.ShirtNumber.ToString();
            lblPosition.Text = player.Position.ToString();
            lblCaptain.Text = player.Captain ? "Captain" : "";
            
            LoadSavedPlayerImage(player);
            player.PlayerImage = pbPlayer.Image;
        }

        private void LoadSavedPlayerImage(StartingEleven player)
        {
            string[] imagesPath = Directory.GetFiles(Path.Combine(repo.GetDefaultPath(), DEFAULT_IMAGE_PATH));
            if (imagesPath.Count() == 0)
            {
                pbPlayer.Image = repo.GetDefaultImage();
            }
            else
            {
                foreach (string imagePath in imagesPath)
                {
                    var imageName = $"{imagePath.Substring(imagePath.IndexOf(DEFAULT_IMAGE_PATH) + DEFAULT_IMAGE_PATH.Length)}";
                    if (player.Name == imageName.Remove(imageName.IndexOf('.')))
                    {
                        using (FileStream fs = new FileStream(imagePath, FileMode.Open))
                        {
                            pbPlayer.Image = Image.FromStream(fs);
                            fs.Close();
                        }
                        return;
                    }
                    else
                    {
                        pbPlayer.Image = repo.GetDefaultImage();
                    }
                } 
            }

        }

        private void PlayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            PlayerControl player = sender as PlayerControl;
            if (e.Button == MouseButtons.Left)
            {
                player.DoDragDrop(player, DragDropEffects.Move);
                selected = !selected;
            }
        }

        private void ChangeImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string playerImagePath = Path.Combine(repo.GetDefaultPath(), DEFAULT_IMAGE_PATH, $"{Player.Name}.jfif");
            
            PictureForm pictureForm = new PictureForm(pbPlayer, PName);
            DialogResult dialogResult = pictureForm.ShowDialog();
            
            if (dialogResult == DialogResult.OK && pictureForm.GetChanged() == 1)
            {
                pbPlayer.Image = pictureForm.GetImage();
                if (File.Exists(playerImagePath))
                {
                    File.Delete(playerImagePath);
                }
                pbPlayer.Image.Save(playerImagePath);
            }
            else if (dialogResult == DialogResult.OK && pictureForm.GetChanged() == 2)
            {
                pbPlayer.Image = repo.GetDefaultImage();
                File.Delete(playerImagePath);
            }
            pictureForm.Dispose();
        }
        public void AddMenuItem(ToolStripMenuItem item) => ContextMenuStrip.Items.Add(item);
    }
}

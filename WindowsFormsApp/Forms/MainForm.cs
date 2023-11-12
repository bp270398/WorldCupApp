
using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.Forms;
using WindowsFormsApp.UserControls;

namespace WindowsFormsApp
{
    public partial class MainForm : Form
    {
        private readonly IRepo repo = RepoFactory.GetRepo();
        private int previousPercentage = 0;
        private int counter = 0;
        private bool changed = false; //prevents save window on no changes
        private const string favouritesR = "Remove from favourites";
        private const string favouritesA = "Add to favourites";

        List<TeamEvent> rankedPlayerControlGoalsList = new List<TeamEvent>();
        List<TeamEvent> rankedPlayerControlYellowCardsList = new List<TeamEvent>();
        List<Match> rankedStadiumControlsList = new List<Match>();
        HashSet<string> favouritesSet = new HashSet<string>();

        public MainForm()
        {
            repo.LoadLanguage();
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            repo.LoadSettings();
            favouritesSet = repo.LoadFavourites();

            if(Settings.language.Equals("Hrvatski"))
            {
                lblGender.Text = Settings.gender ? "Muško svjetsko prvenstvo" : "Žensko svjetsko prventstvo"; 
            }
            else
            {
                lblGender.Text = Settings.gender ? "Mens' World Championship" : "Womens' World Championship";
            }

            FillData();
            lblInfoStatus.Text = "Select a team";

            if (Settings.homeTeamCountry != " ")
            {
                FillPlayerData();
            }
        }

        private void DdlCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.homeTeamCountry = ddlCountries.SelectedItem.ToString().Substring(0, ddlCountries.SelectedItem.ToString().IndexOf("[")).Trim();
            repo.SaveSettings();
            FillPlayerData();
        }

        private async void FillData()
        {
            try
            {
                lblInfoStatus.Text = "Fetching data, please wait...";
                HashSet<Teams> teams = await repo.LoadTeams();

                foreach (Teams team in teams)
                    ddlCountries.Items.Add(team);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                lblInfoStatus.Text = e.Message;
            }
        }
        private async void FillPlayerData()
        {
            lblInfoStatus.Text = "Fetching player data, please wait...";

            pnlPlayersContainer.Controls.Clear();
            pnlRankListContainerGoals.Controls.Clear();
            pnlRankListContainerYellowCards.Controls.Clear();
            pnlStadAttRankListContainer.Controls.Clear();
            pnlFavouritesContainer.Controls.Clear();

            pbLoading.Visible = true;

            try
            {
                HashSet<Match> matches = await repo.LoadMatches();
                lblSelectedCountry.Text = Settings.homeTeamCountry;

                HashSet<StartingEleven> playerList = new HashSet<StartingEleven>();
                HashSet<TeamEvent> rankedPlayerList = new HashSet<TeamEvent>();
                HashSet<Match> rankedStadiumList = new HashSet<Match>();

                HashSet<PlayerControl> playerControls = new HashSet<PlayerControl>();
                HashSet<RankedPlayerControl> rankedPlayersControlGoals = new HashSet<RankedPlayerControl>();
                HashSet<RankedPlayerControl> rankedPlayersControlYellowCards = new HashSet<RankedPlayerControl>();
                HashSet<RankedStadiumControl> rankedStadiumControls = new HashSet<RankedStadiumControl>();

                rankedPlayerControlGoalsList = new List<TeamEvent>();
                rankedPlayerControlYellowCardsList = new List<TeamEvent>();
                rankedStadiumControlsList = new List<Match>();


                //Load match details
                foreach (Match match in matches)
                {
                    UpdateProgress(matches.Count());
                    if (match.HomeTeamStatistics.Country == Settings.homeTeamCountry)
                    {
                        rankedStadiumList.Add(match);
                        AddItemsToList(playerList, match.HomeTeamStatistics.StartingEleven);
                        AddItemsToList(playerList, match.HomeTeamStatistics.Substitutes);
                        AddItemsToList(rankedPlayerList, match.HomeTeamEvents);
                    }
                    if (match.AwayTeamStatistics.Country == Settings.homeTeamCountry)
                    {
                        rankedStadiumList.Add(match);
                        AddItemsToList(playerList, match.AwayTeamStatistics.StartingEleven);
                        AddItemsToList(playerList, match.AwayTeamStatistics.Substitutes);
                        AddItemsToList(rankedPlayerList, match.AwayTeamEvents);
                    }
                }

                //Load players to List
                IEnumerable<StartingEleven> sortedPlayerList = playerList.OrderBy(Item => Item.ShirtNumber);

                foreach (var playerItem in sortedPlayerList)
                {
                    UpdateProgress(sortedPlayerList.Count());
                    var rankedPlayer = new TeamEvent();

                    playerControls.Add(new PlayerControl(playerItem));
                    foreach (var rankedItem in rankedPlayerList.Where(rankedItem => playerItem.Name == rankedItem.Player))
                    {
                        rankedPlayer.Player = playerItem.Name;
                        switch (rankedItem.TypeOfEvent)
                        {
                            case "goal":
                                rankedPlayer.Goals++;
                                break;
                            case "yellow-card":
                                rankedPlayer.YellowCards++;
                                break;
                        }
                    }

                    if (rankedPlayer.Goals != 0)
                    {
                        rankedPlayersControlGoals.Add(new RankedPlayerControl(playerItem, rankedPlayer.Goals));
                        rankedPlayerControlGoalsList.Add(rankedPlayer);
                    }
                    if (rankedPlayer.YellowCards != 0)
                    {
                        rankedPlayersControlYellowCards.Add(new RankedPlayerControl(playerItem, rankedPlayer.YellowCards));
                        rankedPlayerControlYellowCardsList.Add(rankedPlayer);
                    }
                }

                //Load players to App
                foreach (var item in playerControls)
                {
                    UpdateProgress(playerControls.Count());
                    item.AddMenuItem(CreateFavouritesItem(favouritesA));
                    pnlPlayersContainer.Controls.Add(item);
                    foreach (var favourite in favouritesSet.Where(favourite => item.Player.Name == favourite).Select(favourite => new { }))
                    {
                        ChangeMenuItem(item, favouritesR);
                        pnlFavouritesContainer.Controls.Add(item);
                    }
                }

                //Load ranked players to App
                IEnumerable<RankedPlayerControl> SortedRankedPlayerControlGoals = rankedPlayersControlGoals.ToList().OrderBy(i => -i.Result);
                IEnumerable<RankedPlayerControl> SortedRankedPlayerControlYellowCards = rankedPlayersControlYellowCards.ToList().OrderBy(i => -i.Result);
                foreach (var rankedPlayerControl in SortedRankedPlayerControlGoals)
                {
                    UpdateProgress(SortedRankedPlayerControlGoals.Count());
                    pnlRankListContainerGoals.Controls.Add(rankedPlayerControl);
                }
                foreach (var rankedPlayerControl in SortedRankedPlayerControlYellowCards)
                {
                    UpdateProgress(SortedRankedPlayerControlYellowCards.Count());
                    pnlRankListContainerYellowCards.Controls.Add(rankedPlayerControl);
                }

                //Load stadiums to list
                IEnumerable<Match> sortedRankedStadiumList = rankedStadiumList.OrderBy(i => -i.Attendance);
                foreach (var rankedStadium in sortedRankedStadiumList)
                {
                    UpdateProgress(sortedRankedStadiumList.Count());
                    rankedStadiumControls.Add(new RankedStadiumControl(rankedStadium));
                    rankedStadiumControlsList.Add(rankedStadium);
                }
                //Load stadiums to App
                foreach (var rankedStadium in rankedStadiumControls)
                {
                    UpdateProgress(rankedStadiumControls.Count());
                    pnlStadAttRankListContainer.Controls.Add(rankedStadium);
                }
                lblInfoStatus.Text = "Data fetched";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                lblInfoStatus.Text = e.Message;
            }
            pbLoading.Visible = false;
        }

        private static void AddItemsToList<T>(HashSet<T> list, List<T> teamList) => teamList.ForEach(i => list.Add(i));

        private ToolStripMenuItem CreateFavouritesItem(string text)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem
            {
                Text = text,
                Name = "toolStripMenuFavourites"
            };
            menuItem.Click += MenuItemClickHandler;
            return menuItem;
        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            HashSet<PlayerControl> selectedPlayers = new HashSet<PlayerControl>();

            switch (menuItem.Text)
            {
                case favouritesA:
                    AddToSelectedPlayers(selectedPlayers, pnlPlayersContainer, pnlFavouritesContainer);
                    break;
                default:
                    AddToSelectedPlayers(selectedPlayers, pnlFavouritesContainer, pnlPlayersContainer);
                    break;
            }

            foreach (var item in selectedPlayers)
            {
                if (menuItem.Text == favouritesA && pnlFavouritesContainer.Controls.Count < 3)
                {
                    pnlFavouritesContainer.Controls.Add(item);
                    item.selected = false;
                    ChangeMenuItem(item, favouritesR);
                    changed = true;
                }
                else if (menuItem.Text == favouritesR)
                {
                    pnlPlayersContainer.Controls.Add(item);
                    item.selected = false;
                    ChangeMenuItem(item, favouritesA);
                    changed = true;
                }
                else
                {
                    MessageBox.Show($"Favourites limit exceeded, please remove existing before adding new!", "Error: limit reached1");
                }
            }

        }
        private void ChangeMenuItem(PlayerControl playerControl, string favouritesConst)
        {
            playerControl.ContextMenuStrip.Items.RemoveAt(1);
            playerControl.AddMenuItem(CreateFavouritesItem(favouritesConst));
        }
        private void AddToSelectedPlayers(HashSet<PlayerControl> selectedPlayers, FlowLayoutPanel addContainer, FlowLayoutPanel removeContainer)
        {
            foreach (PlayerControl player in addContainer.Controls)
            {
                if (player.selected)
                {
                    selectedPlayers.Add(player);
                }
                foreach (PlayerControl item in removeContainer.Controls)
                {
                    item.selected = false;
                }
            }
        }

        private void PnlContainer_DragEnter(object sender, DragEventArgs e)
        {
            lblInfoStatus.Text = "Drag started";
            e.Effect = DragDropEffects.Move;
        }
        private void PnlPlayersContainer_DragDrop(object sender, DragEventArgs e)
        {
            var playerControl = (PlayerControl)e.Data.GetData(typeof(PlayerControl));

            if (playerControl.Parent == pnlFavouritesContainer)
            {
                lblInfoStatus.Text = "Drop allowed";
                ChangeMenuItem(playerControl, favouritesA);
                pnlPlayersContainer.Controls.Add(playerControl);
                favouritesSet.Remove(playerControl.PName);
                playerControl.selected = true;
                changed = true;
            }
            else
            {
                lblInfoStatus.Text = "Drop not allowed";
            }
        }
        private void PnlFavouritesContainer_DragDrop(object sender, DragEventArgs e)
        {
            var playerControl = (PlayerControl)e.Data.GetData(typeof(PlayerControl));
            if (playerControl.Parent == pnlPlayersContainer && pnlFavouritesContainer.Controls.Count < 3)
            {
                ChangeMenuItem(playerControl, favouritesR);
                pnlFavouritesContainer.Controls.Add(playerControl);
                favouritesSet.Add(playerControl.PName);
                playerControl.selected = true;
                changed = true;
            }
            else
            {
                lblInfoStatus.Text = "Drop not allowed";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changed)
            {
                DialogResult result = MessageBox.Show("You are about to exit, do you want to save your changes before exiting?", "Exit?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.Yes:
                        repo.SaveFavourites(favouritesSet);
                        Dispose();
                        Application.Exit();
                        break;
                    case DialogResult.No:
                        Dispose();
                        Application.Exit();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                } 
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape && changed)
            {
                DialogResult result = MessageBox.Show("You are about to exit, do you want to save your changes before exiting?", "Exit?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.Yes:
                        repo.SaveFavourites(favouritesSet);
                        Dispose();
                        Application.Exit();
                        break;
                    case DialogResult.No:
                        Dispose();
                        Application.Exit();
                        break;
                    case DialogResult.Cancel:
                        return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void UpdateProgress(int max)
        {
            if (max == 1 || max == 0) return;
            int currentPercentage = (int)((double)counter++ / (max - 1) * 100);
            if (currentPercentage != previousPercentage)
            {
                backgroundWorker.ReportProgress(currentPercentage);
            }
            previousPercentage = currentPercentage;
            if (counter == max)
            {
                counter = 0;
            }
        }
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblPercentage.Text = progressBar.Value + "%";
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repo.SaveFavourites(favouritesSet);
            repo.SaveSettings();
            var settingsForm = new SettingsForm();
            this.Visible = false;
            settingsForm.ShowDialog();
            this.Close();
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            int x = 200;
            int y = 100;

            e.Graphics.DrawString("Goals List:", Font, Brushes.Black, x, y += 50);
            rankedPlayerControlGoalsList.OrderBy(i => -i.Goals).ToList()
                .ForEach(player =>
                {
                    e.Graphics.DrawString("Name: " + player.Player
                        + ", Goals: " + player.Goals,
                        Font, Brushes.Black, x, y += 20);
                });
            e.Graphics.DrawString("Yellow Cards List:", Font, Brushes.Black, x, y += 50);
            rankedPlayerControlYellowCardsList.OrderBy(i => -i.YellowCards).ToList()
                .ForEach(player =>
                {
                    e.Graphics.DrawString("Name: " + player.Player
                        + ", Yellow cards: " + player.YellowCards,
                        Font, Brushes.Black, x, y += 20);
                });

            e.Graphics.DrawString("Stadium attendances rank list:", Font, Brushes.Black, x, y += 50);
            rankedStadiumControlsList
                .ForEach(stadium =>
                {
                    e.Graphics.DrawString("Location: " + stadium.Location
                        + ", Home team: " + stadium.HomeTeamCountry
                        + ", Away team: " + stadium.AwayTeamCountry
                        + ", Attendance: " + stadium.Attendance,
                        Font, Brushes.Black, x, y += 20);
                });
        }
        private void ChoosePrintTypeToolStripMenuItem_Click(object sender, EventArgs e)
        => printDialog.ShowDialog();
        private void PreviewPrintToolStripMenuItem_Click(object sender, EventArgs e)
        => printPreviewDialog.ShowDialog();
        private void PrintToolStripMenuItem1_Click(object sender, EventArgs e)
        => printDocument.Print();
    }
}

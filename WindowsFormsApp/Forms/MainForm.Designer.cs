namespace WindowsFormsApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.choosePrintTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewPrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlCountries = new System.Windows.Forms.ComboBox();
            this.lblSelectedCountry = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlPlayersContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFavouritesContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlRankListContainerGoals = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlStadAttRankListContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblPercentage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblInfoStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lblGender = new System.Windows.Forms.Label();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.pnlRankListContainerYellowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.printToolStripMenuItem});
            this.menuStrip.Name = "menuStrip";
            // 
            // settingsToolStripMenuItem
            // 
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
            this.printToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.choosePrintTypeToolStripMenuItem,
            this.previewPrintToolStripMenuItem,
            this.printToolStripMenuItem1});
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            // 
            // choosePrintTypeToolStripMenuItem
            // 
            resources.ApplyResources(this.choosePrintTypeToolStripMenuItem, "choosePrintTypeToolStripMenuItem");
            this.choosePrintTypeToolStripMenuItem.Name = "choosePrintTypeToolStripMenuItem";
            this.choosePrintTypeToolStripMenuItem.Click += new System.EventHandler(this.ChoosePrintTypeToolStripMenuItem_Click);
            // 
            // previewPrintToolStripMenuItem
            // 
            resources.ApplyResources(this.previewPrintToolStripMenuItem, "previewPrintToolStripMenuItem");
            this.previewPrintToolStripMenuItem.Name = "previewPrintToolStripMenuItem";
            this.previewPrintToolStripMenuItem.Click += new System.EventHandler(this.PreviewPrintToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem1
            // 
            resources.ApplyResources(this.printToolStripMenuItem1, "printToolStripMenuItem1");
            this.printToolStripMenuItem1.Name = "printToolStripMenuItem1";
            this.printToolStripMenuItem1.Click += new System.EventHandler(this.PrintToolStripMenuItem1_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ddlCountries
            // 
            resources.ApplyResources(this.ddlCountries, "ddlCountries");
            this.ddlCountries.FormattingEnabled = true;
            this.ddlCountries.Name = "ddlCountries";
            this.ddlCountries.SelectedIndexChanged += new System.EventHandler(this.DdlCountries_SelectedIndexChanged);
            // 
            // lblSelectedCountry
            // 
            resources.ApplyResources(this.lblSelectedCountry, "lblSelectedCountry");
            this.lblSelectedCountry.Name = "lblSelectedCountry";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // pnlPlayersContainer
            // 
            resources.ApplyResources(this.pnlPlayersContainer, "pnlPlayersContainer");
            this.pnlPlayersContainer.AllowDrop = true;
            this.pnlPlayersContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlayersContainer.Name = "pnlPlayersContainer";
            this.pnlPlayersContainer.DragDrop += new System.Windows.Forms.DragEventHandler(this.PnlPlayersContainer_DragDrop);
            this.pnlPlayersContainer.DragEnter += new System.Windows.Forms.DragEventHandler(this.PnlContainer_DragEnter);
            // 
            // pnlFavouritesContainer
            // 
            resources.ApplyResources(this.pnlFavouritesContainer, "pnlFavouritesContainer");
            this.pnlFavouritesContainer.AllowDrop = true;
            this.pnlFavouritesContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFavouritesContainer.Name = "pnlFavouritesContainer";
            this.pnlFavouritesContainer.DragDrop += new System.Windows.Forms.DragEventHandler(this.PnlFavouritesContainer_DragDrop);
            this.pnlFavouritesContainer.DragEnter += new System.Windows.Forms.DragEventHandler(this.PnlContainer_DragEnter);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // pnlRankListContainerGoals
            // 
            resources.ApplyResources(this.pnlRankListContainerGoals, "pnlRankListContainerGoals");
            this.pnlRankListContainerGoals.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRankListContainerGoals.Name = "pnlRankListContainerGoals";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // pnlStadAttRankListContainer
            // 
            resources.ApplyResources(this.pnlStadAttRankListContainer, "pnlStadAttRankListContainer");
            this.pnlStadAttRankListContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStadAttRankListContainer.Name = "pnlStadAttRankListContainer";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.lblPercentage,
            this.lblInfoStatus});
            this.statusStrip.Name = "statusStrip";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lblPercentage
            // 
            resources.ApplyResources(this.lblPercentage, "lblPercentage");
            this.lblPercentage.Name = "lblPercentage";
            // 
            // lblInfoStatus
            // 
            resources.ApplyResources(this.lblInfoStatus, "lblInfoStatus");
            this.lblInfoStatus.Name = "lblInfoStatus";
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument_PrintPage);
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // printPreviewDialog
            // 
            resources.ApplyResources(this.printPreviewDialog, "printPreviewDialog");
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Name = "printPreviewDialog1";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            // 
            // lblGender
            // 
            resources.ApplyResources(this.lblGender, "lblGender");
            this.lblGender.Name = "lblGender";
            // 
            // pbLoading
            // 
            resources.ApplyResources(this.pbLoading, "pbLoading");
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.TabStop = false;
            // 
            // pnlRankListContainerYellowCards
            // 
            resources.ApplyResources(this.pnlRankListContainerYellowCards, "pnlRankListContainerYellowCards");
            this.pnlRankListContainerYellowCards.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRankListContainerYellowCards.Name = "pnlRankListContainerYellowCards";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlRankListContainerYellowCards);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbLoading);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.pnlRankListContainerGoals);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pnlFavouritesContainer);
            this.Controls.Add(this.pnlStadAttRankListContainer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlPlayersContainer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSelectedCountry);
            this.Controls.Add(this.ddlCountries);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem choosePrintTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewPrintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlCountries;
        private System.Windows.Forms.Label lblSelectedCountry;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel pnlPlayersContainer;
        private System.Windows.Forms.FlowLayoutPanel pnlFavouritesContainer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel pnlRankListContainerGoals;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel pnlStadAttRankListContainer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblInfoStatus;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel lblPercentage;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.PictureBox pbLoading;
        private System.Windows.Forms.FlowLayoutPanel pnlRankListContainerYellowCards;
        private System.Windows.Forms.Label label2;
    }
}


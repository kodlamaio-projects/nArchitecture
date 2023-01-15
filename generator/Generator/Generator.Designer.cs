namespace Generator;

partial class Generator
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
        this.EntityListBox = new System.Windows.Forms.CheckedListBox();
        this.CoreEntitiesCheckBox = new System.Windows.Forms.CheckBox();
        this.EntitiesCheckBox = new System.Windows.Forms.CheckBox();
        this.SelectAllCheckBox = new System.Windows.Forms.CheckBox();
        this.InverseCheckBox = new System.Windows.Forms.CheckBox();
        this.EntitiesGroupBox = new System.Windows.Forms.GroupBox();
        this.SelectedEntitiesCounter = new System.Windows.Forms.Label();
        this.SelectedEntities = new System.Windows.Forms.Label();
        this.NumberGroupBox = new System.Windows.Forms.GroupBox();
        this.FileCountLabel = new System.Windows.Forms.Label();
        this.FileCountTextLabel = new System.Windows.Forms.Label();
        this.PahtCountLabel = new System.Windows.Forms.Label();
        this.CountOfPathsTextLabel = new System.Windows.Forms.Label();
        this.EntitiesCounter = new System.Windows.Forms.Label();
        this.Reload = new System.Windows.Forms.Button();
        this.CountOfEntities = new System.Windows.Forms.Label();
        this.DbContextGroupBox = new System.Windows.Forms.GroupBox();
        this.DbContextListBox = new System.Windows.Forms.ListBox();
        this.FileSeletionGroupBox = new System.Windows.Forms.GroupBox();
        this.ConfigLayerLabel = new System.Windows.Forms.Label();
        this.LayerConfigSetButton = new System.Windows.Forms.Button();
        this.NoticeOfFileSelection = new System.Windows.Forms.Label();
        this.GenerateButton = new System.Windows.Forms.Button();
        this.SyncAndAsyncRepoRadioButton = new System.Windows.Forms.RadioButton();
        this.SyncRepoRadioButton = new System.Windows.Forms.RadioButton();
        this.AsyncRepoRadioButton = new System.Windows.Forms.RadioButton();
        this.SimpleRepoRadioButton = new System.Windows.Forms.RadioButton();
        this.RepositorySelectorGroupBox = new System.Windows.Forms.GroupBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.progressBar = new System.Windows.Forms.ProgressBar();
        this.EntitiesGroupBox.SuspendLayout();
        this.NumberGroupBox.SuspendLayout();
        this.DbContextGroupBox.SuspendLayout();
        this.FileSeletionGroupBox.SuspendLayout();
        this.RepositorySelectorGroupBox.SuspendLayout();
        this.SuspendLayout();
        // 
        // EntityListBox
        // 
        this.EntityListBox.FormattingEnabled = true;
        this.EntityListBox.Location = new System.Drawing.Point(6, 53);
        this.EntityListBox.Name = "EntityListBox";
        this.EntityListBox.Size = new System.Drawing.Size(205, 220);
        this.EntityListBox.TabIndex = 4;
        this.EntityListBox.SelectedValueChanged += new System.EventHandler(this.EntityListBox_SelectedValueChanged);
        // 
        // CoreEntitiesCheckBox
        // 
        this.CoreEntitiesCheckBox.AutoSize = true;
        this.CoreEntitiesCheckBox.Location = new System.Drawing.Point(6, 25);
        this.CoreEntitiesCheckBox.Name = "CoreEntitiesCheckBox";
        this.CoreEntitiesCheckBox.Size = new System.Drawing.Size(92, 19);
        this.CoreEntitiesCheckBox.TabIndex = 1;
        this.CoreEntitiesCheckBox.Text = "Core Entities";
        this.CoreEntitiesCheckBox.UseVisualStyleBackColor = true;
        this.CoreEntitiesCheckBox.CheckedChanged += new System.EventHandler(this.CoreEntitiesCheckBox_CheckedChanged);
        // 
        // EntitiesCheckBox
        // 
        this.EntitiesCheckBox.AutoSize = true;
        this.EntitiesCheckBox.Location = new System.Drawing.Point(104, 25);
        this.EntitiesCheckBox.Name = "EntitiesCheckBox";
        this.EntitiesCheckBox.Size = new System.Drawing.Size(107, 19);
        this.EntitiesCheckBox.TabIndex = 2;
        this.EntitiesCheckBox.Text = "Normal Entities";
        this.EntitiesCheckBox.UseVisualStyleBackColor = true;
        this.EntitiesCheckBox.CheckedChanged += new System.EventHandler(this.EntitiesCheckBox_CheckedChanged);
        // 
        // SelectAllCheckBox
        // 
        this.SelectAllCheckBox.AutoSize = true;
        this.SelectAllCheckBox.Location = new System.Drawing.Point(6, 279);
        this.SelectAllCheckBox.Name = "SelectAllCheckBox";
        this.SelectAllCheckBox.Size = new System.Drawing.Size(74, 19);
        this.SelectAllCheckBox.TabIndex = 3;
        this.SelectAllCheckBox.Text = "Select All";
        this.SelectAllCheckBox.UseVisualStyleBackColor = true;
        this.SelectAllCheckBox.CheckedChanged += new System.EventHandler(this.SelectAllCheckBox_CheckedChanged);
        // 
        // InverseCheckBox
        // 
        this.InverseCheckBox.AutoSize = true;
        this.InverseCheckBox.Location = new System.Drawing.Point(104, 279);
        this.InverseCheckBox.Name = "InverseCheckBox";
        this.InverseCheckBox.Size = new System.Drawing.Size(63, 19);
        this.InverseCheckBox.TabIndex = 4;
        this.InverseCheckBox.Text = "Inverse";
        this.InverseCheckBox.UseVisualStyleBackColor = true;
        this.InverseCheckBox.CheckedChanged += new System.EventHandler(this.InverseCheckBox_CheckedChanged);
        // 
        // EntitiesGroupBox
        // 
        this.EntitiesGroupBox.Controls.Add(this.SelectedEntitiesCounter);
        this.EntitiesGroupBox.Controls.Add(this.SelectedEntities);
        this.EntitiesGroupBox.Controls.Add(this.NumberGroupBox);
        this.EntitiesGroupBox.Controls.Add(this.EntitiesCounter);
        this.EntitiesGroupBox.Controls.Add(this.CoreEntitiesCheckBox);
        this.EntitiesGroupBox.Controls.Add(this.EntitiesCheckBox);
        this.EntitiesGroupBox.Controls.Add(this.SelectAllCheckBox);
        this.EntitiesGroupBox.Controls.Add(this.InverseCheckBox);
        this.EntitiesGroupBox.Controls.Add(this.Reload);
        this.EntitiesGroupBox.Controls.Add(this.CountOfEntities);
        this.EntitiesGroupBox.Controls.Add(this.EntityListBox);
        this.EntitiesGroupBox.Location = new System.Drawing.Point(15, 15);
        this.EntitiesGroupBox.Name = "EntitiesGroupBox";
        this.EntitiesGroupBox.Size = new System.Drawing.Size(220, 500);
        this.EntitiesGroupBox.TabIndex = 0;
        this.EntitiesGroupBox.TabStop = false;
        this.EntitiesGroupBox.Text = "Entity";
        // 
        // SelectedEntitiesCounter
        // 
        this.SelectedEntitiesCounter.AutoSize = true;
        this.SelectedEntitiesCounter.Location = new System.Drawing.Point(171, 332);
        this.SelectedEntitiesCounter.Name = "SelectedEntitiesCounter";
        this.SelectedEntitiesCounter.Size = new System.Drawing.Size(13, 15);
        this.SelectedEntitiesCounter.TabIndex = 9;
        this.SelectedEntitiesCounter.Text = "0";
        this.SelectedEntitiesCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // SelectedEntities
        // 
        this.SelectedEntities.AutoSize = true;
        this.SelectedEntities.Location = new System.Drawing.Point(70, 332);
        this.SelectedEntities.Name = "SelectedEntities";
        this.SelectedEntities.Size = new System.Drawing.Size(95, 15);
        this.SelectedEntities.TabIndex = 8;
        this.SelectedEntities.Text = "Selected Entities:";
        this.SelectedEntities.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // NumberGroupBox
        // 
        this.NumberGroupBox.Controls.Add(this.FileCountLabel);
        this.NumberGroupBox.Controls.Add(this.FileCountTextLabel);
        this.NumberGroupBox.Controls.Add(this.PahtCountLabel);
        this.NumberGroupBox.Controls.Add(this.CountOfPathsTextLabel);
        this.NumberGroupBox.Location = new System.Drawing.Point(6, 353);
        this.NumberGroupBox.Name = "NumberGroupBox";
        this.NumberGroupBox.Size = new System.Drawing.Size(205, 141);
        this.NumberGroupBox.TabIndex = 3;
        this.NumberGroupBox.TabStop = false;
        this.NumberGroupBox.Text = "Numbers";
        // 
        // FileCountLabel
        // 
        this.FileCountLabel.AutoSize = true;
        this.FileCountLabel.Location = new System.Drawing.Point(96, 48);
        this.FileCountLabel.Name = "FileCountLabel";
        this.FileCountLabel.Size = new System.Drawing.Size(36, 15);
        this.FileCountLabel.TabIndex = 4;
        this.FileCountLabel.Text = "None";
        // 
        // FileCountTextLabel
        // 
        this.FileCountTextLabel.AutoSize = true;
        this.FileCountTextLabel.Location = new System.Drawing.Point(6, 48);
        this.FileCountTextLabel.Name = "FileCountTextLabel";
        this.FileCountTextLabel.Size = new System.Drawing.Size(84, 15);
        this.FileCountTextLabel.TabIndex = 3;
        this.FileCountTextLabel.Text = ".cs File Count :";
        // 
        // PahtCountLabel
        // 
        this.PahtCountLabel.AutoSize = true;
        this.PahtCountLabel.Location = new System.Drawing.Point(90, 24);
        this.PahtCountLabel.Name = "PahtCountLabel";
        this.PahtCountLabel.Size = new System.Drawing.Size(36, 15);
        this.PahtCountLabel.TabIndex = 2;
        this.PahtCountLabel.Text = "None";
        // 
        // CountOfPathsTextLabel
        // 
        this.CountOfPathsTextLabel.AutoSize = true;
        this.CountOfPathsTextLabel.Location = new System.Drawing.Point(6, 24);
        this.CountOfPathsTextLabel.Name = "CountOfPathsTextLabel";
        this.CountOfPathsTextLabel.Size = new System.Drawing.Size(78, 15);
        this.CountOfPathsTextLabel.TabIndex = 1;
        this.CountOfPathsTextLabel.Text = "Paths Count :";
        // 
        // EntitiesCounter
        // 
        this.EntitiesCounter.AutoSize = true;
        this.EntitiesCounter.Location = new System.Drawing.Point(176, 308);
        this.EntitiesCounter.Name = "EntitiesCounter";
        this.EntitiesCounter.Size = new System.Drawing.Size(13, 15);
        this.EntitiesCounter.TabIndex = 7;
        this.EntitiesCounter.Text = "0";
        this.EntitiesCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // Reload
        // 
        this.Reload.Location = new System.Drawing.Point(6, 308);
        this.Reload.Name = "Reload";
        this.Reload.Size = new System.Drawing.Size(58, 39);
        this.Reload.TabIndex = 5;
        this.Reload.Text = "Reload";
        this.Reload.UseVisualStyleBackColor = true;
        this.Reload.Click += new System.EventHandler(this.Reload_Click);
        // 
        // CountOfEntities
        // 
        this.CountOfEntities.AutoSize = true;
        this.CountOfEntities.Location = new System.Drawing.Point(70, 308);
        this.CountOfEntities.Name = "CountOfEntities";
        this.CountOfEntities.Size = new System.Drawing.Size(100, 15);
        this.CountOfEntities.TabIndex = 6;
        this.CountOfEntities.Text = "Count Of Entities:";
        this.CountOfEntities.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // DbContextGroupBox
        // 
        this.DbContextGroupBox.Controls.Add(this.DbContextListBox);
        this.DbContextGroupBox.Location = new System.Drawing.Point(255, 15);
        this.DbContextGroupBox.Name = "DbContextGroupBox";
        this.DbContextGroupBox.Size = new System.Drawing.Size(200, 85);
        this.DbContextGroupBox.TabIndex = 1;
        this.DbContextGroupBox.TabStop = false;
        this.DbContextGroupBox.Text = "DbContext";
        // 
        // DbContextListBox
        // 
        this.DbContextListBox.FormattingEnabled = true;
        this.DbContextListBox.ItemHeight = 15;
        this.DbContextListBox.Location = new System.Drawing.Point(6, 22);
        this.DbContextListBox.Name = "DbContextListBox";
        this.DbContextListBox.Size = new System.Drawing.Size(188, 49);
        this.DbContextListBox.TabIndex = 0;
        this.DbContextListBox.SelectedIndexChanged += new System.EventHandler(this.DbContextListBox_SelectedIndexChanged);
        // 
        // FileSeletionGroupBox
        // 
        this.FileSeletionGroupBox.Controls.Add(this.ConfigLayerLabel);
        this.FileSeletionGroupBox.Controls.Add(this.LayerConfigSetButton);
        this.FileSeletionGroupBox.Controls.Add(this.NoticeOfFileSelection);
        this.FileSeletionGroupBox.Location = new System.Drawing.Point(255, 223);
        this.FileSeletionGroupBox.Name = "FileSeletionGroupBox";
        this.FileSeletionGroupBox.Size = new System.Drawing.Size(200, 112);
        this.FileSeletionGroupBox.TabIndex = 2;
        this.FileSeletionGroupBox.TabStop = false;
        this.FileSeletionGroupBox.Text = "File Seletion";
        // 
        // ConfigLayerLabel
        // 
        this.ConfigLayerLabel.AutoSize = true;
        this.ConfigLayerLabel.Location = new System.Drawing.Point(6, 92);
        this.ConfigLayerLabel.Name = "ConfigLayerLabel";
        this.ConfigLayerLabel.Size = new System.Drawing.Size(112, 15);
        this.ConfigLayerLabel.TabIndex = 5;
        this.ConfigLayerLabel.Text = "Not Selected .csProj";
        // 
        // LayerConfigSetButton
        // 
        this.LayerConfigSetButton.Location = new System.Drawing.Point(6, 64);
        this.LayerConfigSetButton.Name = "LayerConfigSetButton";
        this.LayerConfigSetButton.Size = new System.Drawing.Size(137, 25);
        this.LayerConfigSetButton.TabIndex = 4;
        this.LayerConfigSetButton.Text = "Set App Any .CsProj File";
        this.LayerConfigSetButton.UseVisualStyleBackColor = true;
        this.LayerConfigSetButton.Click += new System.EventHandler(this.LayerConfigSetButton_Click);
        // 
        // NoticeOfFileSelection
        // 
        this.NoticeOfFileSelection.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
        this.NoticeOfFileSelection.Location = new System.Drawing.Point(6, 19);
        this.NoticeOfFileSelection.Name = "NoticeOfFileSelection";
        this.NoticeOfFileSelection.Size = new System.Drawing.Size(188, 38);
        this.NoticeOfFileSelection.TabIndex = 0;
        this.NoticeOfFileSelection.Text = "Please select the required files below.";
        // 
        // GenerateButton
        // 
        this.GenerateButton.Enabled = false;
        this.GenerateButton.Location = new System.Drawing.Point(255, 360);
        this.GenerateButton.Name = "GenerateButton";
        this.GenerateButton.Size = new System.Drawing.Size(200, 71);
        this.GenerateButton.TabIndex = 4;
        this.GenerateButton.Text = "Generate !!!";
        this.GenerateButton.UseVisualStyleBackColor = true;
        this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
        // 
        // SyncAndAsyncRepoRadioButton
        // 
        this.SyncAndAsyncRepoRadioButton.AutoSize = true;
        this.SyncAndAsyncRepoRadioButton.Location = new System.Drawing.Point(23, 31);
        this.SyncAndAsyncRepoRadioButton.Name = "SyncAndAsyncRepoRadioButton";
        this.SyncAndAsyncRepoRadioButton.Size = new System.Drawing.Size(82, 19);
        this.SyncAndAsyncRepoRadioButton.TabIndex = 5;
        this.SyncAndAsyncRepoRadioButton.TabStop = true;
        this.SyncAndAsyncRepoRadioButton.Text = "SyncAsync";
        this.SyncAndAsyncRepoRadioButton.UseVisualStyleBackColor = true;
        this.SyncAndAsyncRepoRadioButton.CheckedChanged += new System.EventHandler(this.SyncAndAsyncRepoRadioButton_CheckedChanged);
        // 
        // SyncRepoRadioButton
        // 
        this.SyncRepoRadioButton.AutoSize = true;
        this.SyncRepoRadioButton.Location = new System.Drawing.Point(121, 31);
        this.SyncRepoRadioButton.Name = "SyncRepoRadioButton";
        this.SyncRepoRadioButton.Size = new System.Drawing.Size(50, 19);
        this.SyncRepoRadioButton.TabIndex = 6;
        this.SyncRepoRadioButton.TabStop = true;
        this.SyncRepoRadioButton.Text = "Sync";
        this.SyncRepoRadioButton.UseVisualStyleBackColor = true;
        this.SyncRepoRadioButton.CheckedChanged += new System.EventHandler(this.SyncRepoRadioButton_CheckedChanged);
        // 
        // AsyncRepoRadioButton
        // 
        this.AsyncRepoRadioButton.AutoSize = true;
        this.AsyncRepoRadioButton.Location = new System.Drawing.Point(23, 66);
        this.AsyncRepoRadioButton.Name = "AsyncRepoRadioButton";
        this.AsyncRepoRadioButton.Size = new System.Drawing.Size(57, 19);
        this.AsyncRepoRadioButton.TabIndex = 7;
        this.AsyncRepoRadioButton.TabStop = true;
        this.AsyncRepoRadioButton.Text = "Async";
        this.AsyncRepoRadioButton.UseVisualStyleBackColor = true;
        this.AsyncRepoRadioButton.CheckedChanged += new System.EventHandler(this.AsyncRepoRadioButton_CheckedChanged);
        // 
        // SimpleRepoRadioButton
        // 
        this.SimpleRepoRadioButton.AutoSize = true;
        this.SimpleRepoRadioButton.Location = new System.Drawing.Point(121, 66);
        this.SimpleRepoRadioButton.Name = "SimpleRepoRadioButton";
        this.SimpleRepoRadioButton.Size = new System.Drawing.Size(61, 19);
        this.SimpleRepoRadioButton.TabIndex = 8;
        this.SimpleRepoRadioButton.TabStop = true;
        this.SimpleRepoRadioButton.Text = "Simple";
        this.SimpleRepoRadioButton.UseVisualStyleBackColor = true;
        this.SimpleRepoRadioButton.CheckedChanged += new System.EventHandler(this.SimpleRepoRadioButton_CheckedChanged);
        // 
        // RepositorySelectorGroupBox
        // 
        this.RepositorySelectorGroupBox.Controls.Add(this.label1);
        this.RepositorySelectorGroupBox.Controls.Add(this.SimpleRepoRadioButton);
        this.RepositorySelectorGroupBox.Controls.Add(this.label2);
        this.RepositorySelectorGroupBox.Controls.Add(this.AsyncRepoRadioButton);
        this.RepositorySelectorGroupBox.Controls.Add(this.SyncRepoRadioButton);
        this.RepositorySelectorGroupBox.Controls.Add(this.SyncAndAsyncRepoRadioButton);
        this.RepositorySelectorGroupBox.Location = new System.Drawing.Point(255, 106);
        this.RepositorySelectorGroupBox.Name = "RepositorySelectorGroupBox";
        this.RepositorySelectorGroupBox.Size = new System.Drawing.Size(200, 102);
        this.RepositorySelectorGroupBox.TabIndex = 9;
        this.RepositorySelectorGroupBox.TabStop = false;
        this.RepositorySelectorGroupBox.Text = "RepositorySelector";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(90, 159);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(36, 15);
        this.label1.TabIndex = 2;
        this.label1.Text = "None";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(6, 159);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(78, 15);
        this.label2.TabIndex = 1;
        this.label2.Text = "Paths Count :";
        // 
        // progressBar
        // 
        this.progressBar.Location = new System.Drawing.Point(255, 479);
        this.progressBar.Name = "progressBar";
        this.progressBar.Size = new System.Drawing.Size(200, 30);
        this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
        this.progressBar.TabIndex = 10;
        // 
        // Generator
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(479, 526);
        this.Controls.Add(this.progressBar);
        this.Controls.Add(this.RepositorySelectorGroupBox);
        this.Controls.Add(this.GenerateButton);
        this.Controls.Add(this.FileSeletionGroupBox);
        this.Controls.Add(this.DbContextGroupBox);
        this.Controls.Add(this.EntitiesGroupBox);
        this.MaximizeBox = false;
        this.MdiChildrenMinimizedAnchorBottom = false;
        this.MinimizeBox = false;
        this.Name = "Generator";
        this.Text = "Generator";
        this.Load += new System.EventHandler(this.Generator_Load);
        this.EntitiesGroupBox.ResumeLayout(false);
        this.EntitiesGroupBox.PerformLayout();
        this.NumberGroupBox.ResumeLayout(false);
        this.NumberGroupBox.PerformLayout();
        this.DbContextGroupBox.ResumeLayout(false);
        this.FileSeletionGroupBox.ResumeLayout(false);
        this.FileSeletionGroupBox.PerformLayout();
        this.RepositorySelectorGroupBox.ResumeLayout(false);
        this.RepositorySelectorGroupBox.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

    private GroupBox EntitiesGroupBox;
    private CheckedListBox EntityListBox;
    private Label CountOfEntities;
    private Button Reload;
    private CheckBox SelectAllCheckBox;
    private CheckBox InverseCheckBox;
    private CheckBox CoreEntitiesCheckBox;
    private CheckBox EntitiesCheckBox;
    private Label EntitiesCounter;
    private Label SelectedEntitiesCounter;
    private Label SelectedEntities;
    private GroupBox DbContextGroupBox;
    private ListBox DbContextListBox;
    private GroupBox FileSeletionGroupBox;
    private Label NoticeOfFileSelection;
    private Label ConfigLayerLabel;
    private Button LayerConfigSetButton;
    private GroupBox NumberGroupBox;
    private Label PahtCountLabel;
    private Label CountOfPathsTextLabel;
    private Button GenerateButton;
    private RadioButton SyncAndAsyncRepoRadioButton;
    private RadioButton SyncRepoRadioButton;
    private RadioButton AsyncRepoRadioButton;
    private RadioButton SimpleRepoRadioButton;
    private GroupBox RepositorySelectorGroupBox;
    private Label label1;
    private Label label2;
    private Label FileCountLabel;
    private Label FileCountTextLabel;
    private ProgressBar progressBar;
}
﻿
namespace win_capture_audio_installer
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.dragBar = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.internetCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.closeButton = new Guna.UI2.WinForms.Guna2Button();
            this.fadeOut = new System.Windows.Forms.Timer(this.components);
            this.sideBar = new Guna.UI2.WinForms.Guna2Panel();
            this.versions = new System.Windows.Forms.Label();
            this.homeButton = new Guna.UI2.WinForms.Guna2Button();
            this.faqButton = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.settingsButton = new Guna.UI2.WinForms.Guna2Button();
            this.appTitle = new System.Windows.Forms.Label();
            this.homeButtonIcon = new FontAwesome.Sharp.IconPictureBox();
            this.faqButtonIcon = new FontAwesome.Sharp.IconPictureBox();
            this.settingsButtonIcon = new FontAwesome.Sharp.IconPictureBox();
            this.fadeIn = new System.Windows.Forms.Timer(this.components);
            this.dragControl = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.tabControl = new Guna.UI2.WinForms.Guna2TabControl();
            this.homePage = new System.Windows.Forms.TabPage();
            this.versionSelector = new Guna.UI2.WinForms.Guna2ComboBox();
            this.statusText = new System.Windows.Forms.Label();
            this.installButton = new Guna.UI2.WinForms.Guna2Button();
            this.faqPage = new System.Windows.Forms.TabPage();
            this.faqScrollBar = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.faqText = new System.Windows.Forms.RichTextBox();
            this.faqPageLabel = new System.Windows.Forms.Label();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.obsInstallLocationSelector = new Guna.UI2.WinForms.Guna2Button();
            this.obsInstallLocLabel = new System.Windows.Forms.Label();
            this.settingsPageLabel = new System.Windows.Forms.Label();
            this.horizontalRule = new System.Windows.Forms.Panel();
            this.scrollTimer = new System.Windows.Forms.Timer(this.components);
            this.sideBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeButtonIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faqButtonIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsButtonIcon)).BeginInit();
            this.tabControl.SuspendLayout();
            this.homePage.SuspendLayout();
            this.faqPage.SuspendLayout();
            this.settingsPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // dragBar
            // 
            this.dragBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.dragBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(6)))), ((int)(((byte)(184)))));
            this.dragBar.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.dragBar.Location = new System.Drawing.Point(0, 0);
            this.dragBar.Name = "dragBar";
            this.dragBar.ShadowDecoration.Parent = this.dragBar;
            this.dragBar.Size = new System.Drawing.Size(743, 12);
            this.dragBar.TabIndex = 0;
            // 
            // internetCheckTimer
            // 
            this.internetCheckTimer.Enabled = true;
            this.internetCheckTimer.Interval = 10;
            this.internetCheckTimer.Tick += new System.EventHandler(this.internetCheckTimer_Tick);
            // 
            // minimizeButton
            // 
            this.minimizeButton.Animated = true;
            this.minimizeButton.BackColor = System.Drawing.Color.Transparent;
            this.minimizeButton.CheckedState.Parent = this.minimizeButton;
            this.minimizeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimizeButton.CustomImages.Parent = this.minimizeButton;
            this.minimizeButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.minimizeButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.minimizeButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.minimizeButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.minimizeButton.DisabledState.Parent = this.minimizeButton;
            this.minimizeButton.FillColor = System.Drawing.Color.Transparent;
            this.minimizeButton.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.minimizeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.minimizeButton.HoverState.Parent = this.minimizeButton;
            this.minimizeButton.Location = new System.Drawing.Point(670, 10);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.ShadowDecoration.Parent = this.minimizeButton;
            this.minimizeButton.Size = new System.Drawing.Size(35, 38);
            this.minimizeButton.TabIndex = 8;
            this.minimizeButton.TabStop = false;
            this.minimizeButton.Text = "-";
            this.minimizeButton.TextFormatNoPrefix = true;
            this.minimizeButton.UseTransparentBackground = true;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Animated = true;
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.CheckedState.Parent = this.closeButton;
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton.CustomImages.Parent = this.closeButton;
            this.closeButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.closeButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.closeButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.closeButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.closeButton.DisabledState.Parent = this.closeButton;
            this.closeButton.FillColor = System.Drawing.Color.Transparent;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.closeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.closeButton.HoverState.Parent = this.closeButton;
            this.closeButton.Location = new System.Drawing.Point(705, 13);
            this.closeButton.Name = "closeButton";
            this.closeButton.ShadowDecoration.Parent = this.closeButton;
            this.closeButton.Size = new System.Drawing.Size(35, 35);
            this.closeButton.TabIndex = 7;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "X";
            this.closeButton.UseTransparentBackground = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // fadeOut
            // 
            this.fadeOut.Interval = 1;
            this.fadeOut.Tick += new System.EventHandler(this.fadeOut_Tick);
            // 
            // sideBar
            // 
            this.sideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.sideBar.Controls.Add(this.versions);
            this.sideBar.Controls.Add(this.homeButton);
            this.sideBar.Controls.Add(this.faqButton);
            this.sideBar.Controls.Add(this.guna2PictureBox1);
            this.sideBar.Controls.Add(this.settingsButton);
            this.sideBar.Controls.Add(this.appTitle);
            this.sideBar.Controls.Add(this.homeButtonIcon);
            this.sideBar.Controls.Add(this.faqButtonIcon);
            this.sideBar.Controls.Add(this.settingsButtonIcon);
            this.sideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sideBar.Location = new System.Drawing.Point(0, 12);
            this.sideBar.Name = "sideBar";
            this.sideBar.ShadowDecoration.Parent = this.sideBar;
            this.sideBar.Size = new System.Drawing.Size(150, 409);
            this.sideBar.TabIndex = 9;
            // 
            // versions
            // 
            this.versions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.versions.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.versions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.versions.Location = new System.Drawing.Point(0, 361);
            this.versions.Name = "versions";
            this.versions.Size = new System.Drawing.Size(150, 48);
            this.versions.TabIndex = 87;
            this.versions.Text = "Windows: ?\r\nOBS: ?";
            this.versions.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // homeButton
            // 
            this.homeButton.Animated = true;
            this.homeButton.BackColor = System.Drawing.Color.Transparent;
            this.homeButton.CheckedState.Parent = this.homeButton;
            this.homeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.homeButton.CustomImages.Parent = this.homeButton;
            this.homeButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.homeButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.homeButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.homeButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.homeButton.DisabledState.Parent = this.homeButton;
            this.homeButton.FillColor = System.Drawing.Color.Transparent;
            this.homeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.homeButton.ForeColor = System.Drawing.Color.White;
            this.homeButton.HoverState.Parent = this.homeButton;
            this.homeButton.Location = new System.Drawing.Point(0, 175);
            this.homeButton.Name = "homeButton";
            this.homeButton.ShadowDecoration.Parent = this.homeButton;
            this.homeButton.Size = new System.Drawing.Size(150, 52);
            this.homeButton.TabIndex = 18;
            this.homeButton.Text = "Home";
            this.homeButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.homeButton.TextOffset = new System.Drawing.Point(58, 0);
            this.homeButton.UseTransparentBackground = true;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // faqButton
            // 
            this.faqButton.Animated = true;
            this.faqButton.BackColor = System.Drawing.Color.Transparent;
            this.faqButton.CheckedState.Parent = this.faqButton;
            this.faqButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.faqButton.CustomImages.Parent = this.faqButton;
            this.faqButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.faqButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.faqButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.faqButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.faqButton.DisabledState.Parent = this.faqButton;
            this.faqButton.FillColor = System.Drawing.Color.Transparent;
            this.faqButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.faqButton.ForeColor = System.Drawing.Color.White;
            this.faqButton.HoverState.Parent = this.faqButton;
            this.faqButton.Location = new System.Drawing.Point(0, 238);
            this.faqButton.Name = "faqButton";
            this.faqButton.ShadowDecoration.Parent = this.faqButton;
            this.faqButton.Size = new System.Drawing.Size(150, 52);
            this.faqButton.TabIndex = 16;
            this.faqButton.Text = "FAQ";
            this.faqButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.faqButton.TextOffset = new System.Drawing.Point(58, 0);
            this.faqButton.UseTransparentBackground = true;
            this.faqButton.Click += new System.EventHandler(this.faqButton_Click);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Empty;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(0, 7);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.ShadowDecoration.Parent = this.guna2PictureBox1;
            this.guna2PictureBox1.Size = new System.Drawing.Size(150, 100);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 15;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.UseTransparentBackground = true;
            // 
            // settingsButton
            // 
            this.settingsButton.Animated = true;
            this.settingsButton.BackColor = System.Drawing.Color.Transparent;
            this.settingsButton.CheckedState.Parent = this.settingsButton;
            this.settingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.settingsButton.CustomImages.Parent = this.settingsButton;
            this.settingsButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.settingsButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.settingsButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.settingsButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.settingsButton.DisabledState.Parent = this.settingsButton;
            this.settingsButton.FillColor = System.Drawing.Color.Transparent;
            this.settingsButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.settingsButton.ForeColor = System.Drawing.Color.White;
            this.settingsButton.HoverState.Parent = this.settingsButton;
            this.settingsButton.Location = new System.Drawing.Point(0, 297);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.ShadowDecoration.Parent = this.settingsButton;
            this.settingsButton.Size = new System.Drawing.Size(150, 52);
            this.settingsButton.TabIndex = 13;
            this.settingsButton.Text = "Settings";
            this.settingsButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.settingsButton.TextOffset = new System.Drawing.Point(58, 0);
            this.settingsButton.UseTransparentBackground = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // appTitle
            // 
            this.appTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appTitle.ForeColor = System.Drawing.Color.White;
            this.appTitle.Location = new System.Drawing.Point(0, 101);
            this.appTitle.Name = "appTitle";
            this.appTitle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.appTitle.Size = new System.Drawing.Size(150, 60);
            this.appTitle.TabIndex = 10;
            this.appTitle.Text = "Win Capture Audio";
            this.appTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // homeButtonIcon
            // 
            this.homeButtonIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.homeButtonIcon.ForeColor = System.Drawing.Color.RoyalBlue;
            this.homeButtonIcon.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.homeButtonIcon.IconColor = System.Drawing.Color.RoyalBlue;
            this.homeButtonIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.homeButtonIcon.IconSize = 48;
            this.homeButtonIcon.Location = new System.Drawing.Point(12, 179);
            this.homeButtonIcon.Name = "homeButtonIcon";
            this.homeButtonIcon.Size = new System.Drawing.Size(48, 48);
            this.homeButtonIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.homeButtonIcon.TabIndex = 19;
            this.homeButtonIcon.TabStop = false;
            // 
            // faqButtonIcon
            // 
            this.faqButtonIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.faqButtonIcon.ForeColor = System.Drawing.Color.RoyalBlue;
            this.faqButtonIcon.IconChar = FontAwesome.Sharp.IconChar.Lightbulb;
            this.faqButtonIcon.IconColor = System.Drawing.Color.RoyalBlue;
            this.faqButtonIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.faqButtonIcon.IconSize = 48;
            this.faqButtonIcon.Location = new System.Drawing.Point(12, 244);
            this.faqButtonIcon.Name = "faqButtonIcon";
            this.faqButtonIcon.Size = new System.Drawing.Size(48, 48);
            this.faqButtonIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.faqButtonIcon.TabIndex = 17;
            this.faqButtonIcon.TabStop = false;
            // 
            // settingsButtonIcon
            // 
            this.settingsButtonIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.settingsButtonIcon.ForeColor = System.Drawing.Color.RoyalBlue;
            this.settingsButtonIcon.IconChar = FontAwesome.Sharp.IconChar.Cog;
            this.settingsButtonIcon.IconColor = System.Drawing.Color.RoyalBlue;
            this.settingsButtonIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.settingsButtonIcon.IconSize = 48;
            this.settingsButtonIcon.Location = new System.Drawing.Point(12, 303);
            this.settingsButtonIcon.Name = "settingsButtonIcon";
            this.settingsButtonIcon.Size = new System.Drawing.Size(48, 48);
            this.settingsButtonIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.settingsButtonIcon.TabIndex = 14;
            this.settingsButtonIcon.TabStop = false;
            // 
            // fadeIn
            // 
            this.fadeIn.Enabled = true;
            this.fadeIn.Interval = 1;
            this.fadeIn.Tick += new System.EventHandler(this.fadeIn_Tick);
            // 
            // dragControl
            // 
            this.dragControl.DockIndicatorTransparencyValue = 0.6D;
            this.dragControl.TargetControl = this.dragBar;
            this.dragControl.TransparentWhileDrag = false;
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl.Controls.Add(this.homePage);
            this.tabControl.Controls.Add(this.faqPage);
            this.tabControl.Controls.Add(this.settingsPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl.ItemSize = new System.Drawing.Size(180, 40);
            this.tabControl.Location = new System.Drawing.Point(150, 49);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(593, 372);
            this.tabControl.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tabControl.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tabControl.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControl.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tabControl.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tabControl.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tabControl.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControl.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControl.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.tabControl.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControl.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tabControl.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabControl.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControl.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tabControl.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tabControl.TabButtonSize = new System.Drawing.Size(180, 40);
            this.tabControl.TabIndex = 10;
            this.tabControl.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControl.TabMenuVisible = false;
            this.tabControl.TabStop = false;
            // 
            // homePage
            // 
            this.homePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.homePage.Controls.Add(this.versionSelector);
            this.homePage.Controls.Add(this.statusText);
            this.homePage.Controls.Add(this.installButton);
            this.homePage.Location = new System.Drawing.Point(5, 4);
            this.homePage.Name = "homePage";
            this.homePage.Padding = new System.Windows.Forms.Padding(3);
            this.homePage.Size = new System.Drawing.Size(584, 364);
            this.homePage.TabIndex = 1;
            this.homePage.Text = "homePage";
            // 
            // versionSelector
            // 
            this.versionSelector.AutoRoundedCorners = true;
            this.versionSelector.BackColor = System.Drawing.Color.Transparent;
            this.versionSelector.BorderRadius = 15;
            this.versionSelector.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.versionSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionSelector.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.versionSelector.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.versionSelector.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.versionSelector.FocusedState.Parent = this.versionSelector;
            this.versionSelector.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.versionSelector.ForeColor = System.Drawing.Color.White;
            this.versionSelector.HoverState.Parent = this.versionSelector;
            this.versionSelector.ItemHeight = 27;
            this.versionSelector.ItemsAppearance.Parent = this.versionSelector;
            this.versionSelector.Location = new System.Drawing.Point(221, 183);
            this.versionSelector.Name = "versionSelector";
            this.versionSelector.ShadowDecoration.Parent = this.versionSelector;
            this.versionSelector.Size = new System.Drawing.Size(141, 33);
            this.versionSelector.TabIndex = 86;
            this.versionSelector.TabStop = false;
            this.versionSelector.SelectedIndexChanged += new System.EventHandler(this.versionSelector_SelectedIndexChanged);
            // 
            // statusText
            // 
            this.statusText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.statusText.Location = new System.Drawing.Point(3, 338);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(578, 23);
            this.statusText.TabIndex = 2;
            this.statusText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // installButton
            // 
            this.installButton.Animated = true;
            this.installButton.AutoRoundedCorners = true;
            this.installButton.BackColor = System.Drawing.Color.Transparent;
            this.installButton.BorderRadius = 21;
            this.installButton.CheckedState.Parent = this.installButton;
            this.installButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.installButton.CustomImages.Parent = this.installButton;
            this.installButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.installButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.installButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.installButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.installButton.DisabledState.Parent = this.installButton;
            this.installButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.installButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installButton.ForeColor = System.Drawing.Color.White;
            this.installButton.HoverState.Parent = this.installButton;
            this.installButton.Location = new System.Drawing.Point(192, 132);
            this.installButton.Name = "installButton";
            this.installButton.ShadowDecoration.Parent = this.installButton;
            this.installButton.Size = new System.Drawing.Size(200, 45);
            this.installButton.TabIndex = 1;
            this.installButton.TabStop = false;
            this.installButton.Text = "Install";
            this.installButton.UseTransparentBackground = true;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // faqPage
            // 
            this.faqPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.faqPage.Controls.Add(this.faqScrollBar);
            this.faqPage.Controls.Add(this.faqText);
            this.faqPage.Controls.Add(this.faqPageLabel);
            this.faqPage.Location = new System.Drawing.Point(5, 4);
            this.faqPage.Name = "faqPage";
            this.faqPage.Size = new System.Drawing.Size(584, 364);
            this.faqPage.TabIndex = 2;
            this.faqPage.Text = "faqPage";
            // 
            // faqScrollBar
            // 
            this.faqScrollBar.AutoRoundedCorners = true;
            this.faqScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.faqScrollBar.BorderRadius = 8;
            this.faqScrollBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.faqScrollBar.HoverState.Parent = null;
            this.faqScrollBar.InUpdate = false;
            this.faqScrollBar.LargeChange = 10;
            this.faqScrollBar.Location = new System.Drawing.Point(564, 64);
            this.faqScrollBar.Name = "faqScrollBar";
            this.faqScrollBar.PressedState.Parent = this.faqScrollBar;
            this.faqScrollBar.ScrollbarSize = 18;
            this.faqScrollBar.Size = new System.Drawing.Size(18, 292);
            this.faqScrollBar.TabIndex = 96;
            this.faqScrollBar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.faqScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.faqScrollBar_Scroll);
            // 
            // faqText
            // 
            this.faqText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.faqText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.faqText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faqText.ForeColor = System.Drawing.Color.Gainsboro;
            this.faqText.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.faqText.Location = new System.Drawing.Point(7, 64);
            this.faqText.Name = "faqText";
            this.faqText.ReadOnly = true;
            this.faqText.RightMargin = 550;
            this.faqText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.faqText.ShortcutsEnabled = false;
            this.faqText.Size = new System.Drawing.Size(594, 292);
            this.faqText.TabIndex = 95;
            this.faqText.TabStop = false;
            this.faqText.Text = "";
            // 
            // faqPageLabel
            // 
            this.faqPageLabel.BackColor = System.Drawing.Color.Transparent;
            this.faqPageLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.faqPageLabel.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faqPageLabel.ForeColor = System.Drawing.Color.White;
            this.faqPageLabel.Location = new System.Drawing.Point(0, 0);
            this.faqPageLabel.Name = "faqPageLabel";
            this.faqPageLabel.Size = new System.Drawing.Size(584, 61);
            this.faqPageLabel.TabIndex = 94;
            this.faqPageLabel.Text = "FAQ";
            this.faqPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // settingsPage
            // 
            this.settingsPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.settingsPage.Controls.Add(this.obsInstallLocationSelector);
            this.settingsPage.Controls.Add(this.obsInstallLocLabel);
            this.settingsPage.Controls.Add(this.settingsPageLabel);
            this.settingsPage.Location = new System.Drawing.Point(5, 4);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Size = new System.Drawing.Size(584, 364);
            this.settingsPage.TabIndex = 4;
            this.settingsPage.Text = "t";
            // 
            // obsInstallLocationSelector
            // 
            this.obsInstallLocationSelector.Animated = true;
            this.obsInstallLocationSelector.AutoRoundedCorners = true;
            this.obsInstallLocationSelector.BackColor = System.Drawing.Color.Transparent;
            this.obsInstallLocationSelector.BorderRadius = 13;
            this.obsInstallLocationSelector.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.ToogleButton;
            this.obsInstallLocationSelector.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(6)))), ((int)(((byte)(184)))));
            this.obsInstallLocationSelector.CheckedState.Parent = this.obsInstallLocationSelector;
            this.obsInstallLocationSelector.Cursor = System.Windows.Forms.Cursors.Hand;
            this.obsInstallLocationSelector.CustomImages.Parent = this.obsInstallLocationSelector;
            this.obsInstallLocationSelector.DisabledState.Parent = this.obsInstallLocationSelector;
            this.obsInstallLocationSelector.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.obsInstallLocationSelector.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.obsInstallLocationSelector.ForeColor = System.Drawing.Color.White;
            this.obsInstallLocationSelector.HoverState.Parent = this.obsInstallLocationSelector;
            this.obsInstallLocationSelector.Location = new System.Drawing.Point(218, 182);
            this.obsInstallLocationSelector.Name = "obsInstallLocationSelector";
            this.obsInstallLocationSelector.ShadowDecoration.Parent = this.obsInstallLocationSelector;
            this.obsInstallLocationSelector.Size = new System.Drawing.Size(141, 29);
            this.obsInstallLocationSelector.TabIndex = 96;
            this.obsInstallLocationSelector.TabStop = false;
            this.obsInstallLocationSelector.Text = "Choose Location";
            this.obsInstallLocationSelector.UseTransparentBackground = true;
            this.obsInstallLocationSelector.Click += new System.EventHandler(this.obsInstallLocationSelector_Click);
            // 
            // obsInstallLocLabel
            // 
            this.obsInstallLocLabel.AutoSize = true;
            this.obsInstallLocLabel.BackColor = System.Drawing.Color.Transparent;
            this.obsInstallLocLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.obsInstallLocLabel.ForeColor = System.Drawing.Color.White;
            this.obsInstallLocLabel.Location = new System.Drawing.Point(195, 149);
            this.obsInstallLocLabel.Name = "obsInstallLocLabel";
            this.obsInstallLocLabel.Size = new System.Drawing.Size(189, 25);
            this.obsInstallLocLabel.TabIndex = 95;
            this.obsInstallLocLabel.Text = "OBS Install Location";
            this.obsInstallLocLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // settingsPageLabel
            // 
            this.settingsPageLabel.BackColor = System.Drawing.Color.Transparent;
            this.settingsPageLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingsPageLabel.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsPageLabel.ForeColor = System.Drawing.Color.White;
            this.settingsPageLabel.Location = new System.Drawing.Point(0, 0);
            this.settingsPageLabel.Name = "settingsPageLabel";
            this.settingsPageLabel.Size = new System.Drawing.Size(584, 61);
            this.settingsPageLabel.TabIndex = 94;
            this.settingsPageLabel.Text = "Settings";
            this.settingsPageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // horizontalRule
            // 
            this.horizontalRule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.horizontalRule.Location = new System.Drawing.Point(151, 46);
            this.horizontalRule.Name = "horizontalRule";
            this.horizontalRule.Size = new System.Drawing.Size(595, 1);
            this.horizontalRule.TabIndex = 11;
            // 
            // scrollTimer
            // 
            this.scrollTimer.Enabled = true;
            this.scrollTimer.Interval = 1;
            this.scrollTimer.Tick += new System.EventHandler(this.scrollTimer_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(743, 421);
            this.Controls.Add(this.horizontalRule);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.sideBar);
            this.Controls.Add(this.dragBar);
            this.Controls.Add(this.minimizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.sideBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.homeButtonIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faqButtonIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsButtonIcon)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.homePage.ResumeLayout(false);
            this.faqPage.ResumeLayout(false);
            this.settingsPage.ResumeLayout(false);
            this.settingsPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2GradientPanel dragBar;
        public System.Windows.Forms.Timer internetCheckTimer;
        public Guna.UI2.WinForms.Guna2Button minimizeButton;
        public Guna.UI2.WinForms.Guna2Button closeButton;
        private System.Windows.Forms.Timer fadeOut;
        private Guna.UI2.WinForms.Guna2Panel sideBar;
        public Guna.UI2.WinForms.Guna2Button settingsButton;
        private System.Windows.Forms.Label appTitle;
        public FontAwesome.Sharp.IconPictureBox settingsButtonIcon;
        public FontAwesome.Sharp.IconPictureBox homeButtonIcon;
        public Guna.UI2.WinForms.Guna2Button homeButton;
        public FontAwesome.Sharp.IconPictureBox faqButtonIcon;
        public Guna.UI2.WinForms.Guna2Button faqButton;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Timer fadeIn;
        private Guna.UI2.WinForms.Guna2DragControl dragControl;
        private Guna.UI2.WinForms.Guna2TabControl tabControl;
        private System.Windows.Forms.TabPage homePage;
        private System.Windows.Forms.TabPage faqPage;
        public System.Windows.Forms.Label faqPageLabel;
        public System.Windows.Forms.Label settingsPageLabel;
        private System.Windows.Forms.Panel horizontalRule;
        public System.Windows.Forms.RichTextBox faqText;
        private Guna.UI2.WinForms.Guna2Button installButton;
        private System.Windows.Forms.Label statusText;
        public Guna.UI2.WinForms.Guna2ComboBox versionSelector;
        public System.Windows.Forms.Label versions;
        public Guna.UI2.WinForms.Guna2Button obsInstallLocationSelector;
        public System.Windows.Forms.Label obsInstallLocLabel;
        public System.Windows.Forms.TabPage settingsPage;
        private Guna.UI2.WinForms.Guna2VScrollBar faqScrollBar;
        private System.Windows.Forms.Timer scrollTimer;
    }
}
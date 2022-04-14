using Guna.UI2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using win_capture_audio_installer.Classes;
using win_capture_audio_installer.Classes.Structs;
using win_capture_audio_installer.Information;
using WK.Libraries.BetterFolderBrowserNS;

namespace win_capture_audio_installer
{
    public partial class MainWindow : Form
    {
        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref Point lParam);

        private const int EM_GETSCROLLPOS = 1245;
        private const int EM_SETSCROLLPOS = 1246;

        public Logger dLogger = new Logger(@"C:\temp\win-capture-audio-installer\Logs", "Default", true, LogLevel.Error, LogLevel.Information, LogLocation.ConsoleAndFile, LogLocation.ConsoleAndFile);
        public static MainWindow INSTANCE;
        public bool internet;

        public Version minOBSVersion = new Version(27, 1, 3);
        public int minWINVersion = 19041;

        public MainWindow()
        {
            INSTANCE = this;

            InitializeComponent();

            ControlManager.Home();
            DownloadRequiredFiles();
        }

        public List<PluginVersion> versionsList = new List<PluginVersion>();

        public void DownloadRequiredFiles()
        {
            Task.Run(async () =>
            {
                UpdateStatus("Getting latest plugin versions...", false);
                List<GithubRelease.Root> latestVersions = Web.GetReleases();

#if DEBUG
                File.Copy("../../../Resources/FAQ.rtf", @"C:\temp\win-capture-audio-installer\Data\FAQ.rtf", true);
                File.Copy("../../../Resources/required.txt", @"C:\temp\win-capture-audio-installer\Data\required.txt", true);
#else
                await DownloadManager.DownloadAsync(
               "https://raw.githubusercontent.com/DeathlyBower959/win-capture-audio-installer/master/win-capture-audio-installer/Resources/FAQ.rtf",
               @"C:\temp\win-capture-audio-installer\Data",
               "FAQ.rtf");

                await DownloadManager.DownloadAsync(
                "https://raw.githubusercontent.com/DeathlyBower959/win-capture-audio-installer/master/win-capture-audio-installer/Resources/required.txt",
                @"C:\temp\win-capture-audio-installer\Data",
                "required.txt");
#endif



                if (File.Exists(@"C:\temp\win-capture-audio-installer\Data\required.txt"))
                {
                    string[] data = File.ReadAllLines(@"C:\temp\win-capture-audio-installer\Data\required.txt");
                    minOBSVersion = new Version(data[0].Split(':')[1]);
                    minWINVersion = int.Parse(data[1].Split(':')[1]);
                    dLogger.Log($"Retrieved required versions for windows and OBS | OBS: {minOBSVersion} | Win: {minWINVersion}", LogLevel.Success);
                }

                if (latestVersions == null)
                {
                    dLogger.Log("Failed to retrieve latest releases!", LogLevel.Error);
                    versionSelector.Invoke(new Action(() =>
                    {
                        versionSelector.Items.Clear();
                    }));
                    UpdateStatus("Failed to retrieve latest plugin versions...");
                    return;
                }

                versionSelector.Invoke(new Action(() =>
                {
                    versionSelector.Items.Clear();
                    versionSelector.Items.Add("Loading...");
                }));

                try
                {
                    foreach (GithubRelease.Root release in latestVersions)
                    {
                        if (release.Assets != null && release.TagName != null)
                        {
                            foreach (GithubRelease.Asset asset in release.Assets)
                            {
                                if (asset.Name != null && asset.BrowserDownloadUrl != null)
                                {
                                    if (asset.Name.Contains("win-capture-audio") && asset.Name.EndsWith(".zip") || asset.Name.EndsWith(".zip"))
                                    {
                                        try
                                        {

                                            versionsList.Add(new PluginVersion() { downloadURL = asset.BrowserDownloadUrl, tag = release.TagName });
                                            versionSelector.Invoke(new Action(() =>
                                            {
                                                if (versionSelector.Items.Contains("Loading...")) versionSelector.Items.Remove("Loading...");
                                                versionSelector.Items.Add(release.TagName);
                                            }));
                                            UpdateStatus($"Found version {release.TagName}");

                                        }
                                        catch (Exception e)
                                        {
                                            UpdateStatus($"Failed to add: {release.TagName}");
                                            dLogger.Log("Failed to add: " + release.TagName, LogLevel.Error);
                                            dLogger.Log(e);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    versionSelector.Invoke(new Action(() =>
                    {
                        versionSelector.SelectedIndex = 0;
                    }));

                    UpdateStatus("Finished retriving plugin versions...");
                }

                catch (Exception e)
                {
                    UpdateStatus("Failed to retrieve releases.");

                    dLogger.Log("Failed to retrieve releases!", LogLevel.Error);
                    dLogger.Log(e);
                }


            });
        }


        private void internetCheckTimer_Tick(object sender, EventArgs e)
        {
            InternetManager.ReCheckInternet();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            closeApp("Closed Via Close Button");
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeApp("Closed: " + e.CloseReason);
        }

        private bool _isClosing = false;
        private string confirmClose = string.Empty;
        private void closeApp(string logText)
        {
            if (_isClosing) return;
            Properties.Settings.Default.Save();
            if (confirmClose == string.Empty)
            {

                _isClosing = true;

                dLogger.Log(logText);

                //Close all loggers
                FieldInfo[] fis = typeof(MainWindow).GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                foreach (FieldInfo fieldInfo in fis)
                {
                    if (fieldInfo.FieldType == typeof(Logger))
                    {
                        var logger = fieldInfo.GetValue(this) as Logger;
                        logger.Disable();
                    }
                }

                fadeOut.Start();
            }
            else
            {
                if (MessageBox.Show(confirmClose, "Are you sure you want to quit?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _isClosing = true;

                    dLogger.Log(logText);

                    //Close all loggers
                    FieldInfo[] fis = typeof(MainWindow).GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                    foreach (FieldInfo fieldInfo in fis)
                    {
                        if (fieldInfo.FieldType == typeof(Logger))
                        {
                            var logger = fieldInfo.GetValue(this) as Logger;
                            logger.Disable();
                        }
                    }

                    fadeOut.Start();
                }
            }
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void fadeOut_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.04; //Fade in
            if (this.Opacity <= 0)
            {
                fadeOut.Stop();
                this.Close();
            }
        }

        private void fadeIn_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.04; //Fade in
            if (this.Opacity >= 1)
            {
                fadeIn.Stop();
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == homePage) return;

            tabControl.SelectedTab = homePage;
            ControlManager.Home();
        }

        private void faqButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == faqPage) return;

            tabControl.SelectedTab = faqPage;
            ControlManager.FAQ();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == settingsPage) return;

            tabControl.SelectedTab = settingsPage;
            ControlManager.Settings();
        }


        CancellationTokenSource prevSource = new CancellationTokenSource();
        public void UpdateStatus(string newStatus, bool autoClose = true)
        {
            if (prevSource != null) prevSource.Cancel();

            CancellationTokenSource newSource = new CancellationTokenSource();
            prevSource = newSource;

            statusText.Invoke(new Action(() =>
            {
                statusText.Text = newStatus;
            }));

            if (autoClose)
                ClearStatus(newSource.Token);
        }

        /// <summary>
        /// RTL: Removes text from the right of the label to the left
        /// DoubleEdge: Removes text from both sides of the label at the same time
        /// Center: Removes the letter in the center of the text
        /// </summary>
        public enum StatusAnimationType
        {
            RTL,
            DoubleEdge,
            Center
        }

        public async void ClearStatus(CancellationToken cancelToken = default(CancellationToken))
        {
            int delay = 3000;
            int textDelay = 35;
            StatusAnimationType animationType = StatusAnimationType.DoubleEdge;

            await Task.Delay(delay);
            statusText.Invoke(new Action(async () =>
            {
                int length = 0;

                if (animationType == StatusAnimationType.RTL)
                    length = statusText.Text.Length;

                else if (animationType == StatusAnimationType.DoubleEdge)
                    length = Convert.ToInt32(Math.Floor(statusText.Text.Length / 2f));

                else if (animationType == StatusAnimationType.Center)
                    length = statusText.Text.Length;

                for (int i = length; i > 0; i--)
                {
                    if (cancelToken != CancellationToken.None && cancelToken.IsCancellationRequested) return;

                    if (animationType == StatusAnimationType.RTL)
                        statusText.Text = statusText.Text.Remove(statusText.Text.Length - 1, 1);

                    else if (animationType == StatusAnimationType.DoubleEdge)
                        statusText.Text = statusText.Text != string.Empty ? statusText.Text?.Remove(statusText.Text.Length - 1, 1)?.Remove(0, 1) : "";

                    else if (animationType == StatusAnimationType.Center)
                        statusText.Text = statusText.Text.Remove((Convert.ToInt32(Math.Floor(statusText.Text.Length / 2f))), 1);

                    await Task.Delay(textDelay);
                }

                statusText.Text = string.Empty;
            }));
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            bool obsCompat = OBS.IsCompatible();
            bool winCompat = Windows.IsCompatible();

            if (!obsCompat || !winCompat) return;

            CaptureAudio.Install(versionSelector.GetItemText(versionSelector.SelectedItem));
        }

        private void obsInstallLocationSelector_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.OBSInstall == "auto")
            {
                BetterFolderBrowser obsLoc = new BetterFolderBrowser
                {
                    Title = "Choose your OBS install location!",
                    RootFolder = @"C:\Program Files",
                    Multiselect = false
                };

                if (obsLoc.ShowDialog() == DialogResult.OK)
                {
                    if (OBS.IsOBSFolder(obsLoc.SelectedFolder))
                    {
                        Properties.Settings.Default.OBSInstall = obsLoc.SelectedFolder;
                        Notify.Toast("OBS Location", "Succesfully changed OBS install location to:\n" + obsLoc.SelectedFolder, 2);
                    }
                    else
                    {
                        Notify.Toast("OBS Location", "Failed to update OBS studio install location...", 2);
                    }
                }
            }
            else
            {
                Properties.Settings.Default.OBSInstall = "auto";
                Notify.Toast("OBS Install", "The install location of OBS Studio as to been set to automatically detect!", 2);
            }

            ((Guna2Button)sender).Checked = Properties.Settings.Default.OBSInstall == "auto" ? false : true;


        }

        private void versionSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (versionSelector.GetItemText(versionSelector.SelectedItem) == "Loading...") versionSelector.SelectedIndex = -1;
        }

        private void faqScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Size size = TextRenderer.MeasureText(faqText.Text, faqText.Font);
            faqScrollBar.Maximum = size.Height + 18;

            Point p = new Point(0, e.NewValue);
            SendMessage(faqText.Handle, EM_SETSCROLLPOS, 0, ref p);
        }

        private void scrollTimer_Tick(object sender, EventArgs e)
        {
            Point p = new Point();
            SendMessage(faqText.Handle, EM_GETSCROLLPOS, 0, ref p);
            faqScrollBar.Value = p.Y;
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            UpdateStatus("Uninstalling the plugin...");
            CaptureAudio.Uninstall();
        }

        private void helpVideoButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=a_gYy27eTTw");
        }
    }
}
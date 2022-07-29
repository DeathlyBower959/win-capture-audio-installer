using Guna.UI2.WinForms;
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

        public DebugLogger dLogger = new DebugLogger(@"C:\temp\win-capture-audio-installer\Logs", "Default", true, LogLevel.Error, LogLevel.Information, LogLocation.ConsoleAndFile, LogLocation.ConsoleAndFile);
        public static MainWindow INSTANCE;
        public bool internet;

        public int minWINVersion = 19041;

        public MainWindow()
        {
            INSTANCE = this;
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();

            ControlManager.Home();
            DownloadRequiredFiles();

            if (!Environment.Is64BitOperatingSystem)
                Notify.Toast("Windows Architecture", "It seems that you are running x32, but this plugin does not support that architecture!");


        }

        public List<PluginVersion> versionsList = new List<PluginVersion>();

        public void DownloadRequiredFiles()
        {
            Task.Run(async () =>
            {
                UpdateStatus("Getting latest plugin versions...", false);
                List<GithubRelease.Root> latestVersions = Web.GetReleases();

#if DEBUG
                DirectoryInfo fileLocation = Directory.GetParent(Directory.GetCurrentDirectory());
                string resourcesPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), fileLocation.Name == "bin" ? @"..\..\Resources" : @"..\..\..\Resources"));


                File.Copy(Path.Combine(resourcesPath, "FAQ.rtf"), @"C:\temp\win-capture-audio-installer\Data\FAQ.rtf", true);
                File.Copy(Path.Combine(resourcesPath, "required.txt"), @"C:\temp\win-capture-audio-installer\Data\required.txt", true);
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
                    minWINVersion = int.Parse(data[0].Split(':')[1]);
                    dLogger.Log($"Retrieved required versions for windows | Win: {minWINVersion}", LogLevel.Success);
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

                List<string> failedToAdd = new List<string>();

                Version latestMinOBSVersion = new Version();

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
                                    if (asset.Name.Contains("win-capture-audio") && asset.Name.EndsWith(".zip"))
                                    {
                                        try
                                        {

                                            int startIndex = release.Body.IndexOf("For OBS versions ") + "For OBS versions ".Length;
                                            int length = release.Body.IndexOf(" and newer") - startIndex;
                                            string parsedMinVersion = release.Body.Substring(startIndex, length);

                                            Version minObsVersion;
                                            Version.TryParse(" ", out minObsVersion);

                                            if (minObsVersion != null) latestMinOBSVersion = minObsVersion;

                                            versionsList.Add(new PluginVersion(asset.BrowserDownloadUrl, release.TagName, latestMinOBSVersion));
                                            versionSelector.Invoke(new Action(() =>
                                            {
                                                if (versionSelector.Items.Contains("Loading...")) versionSelector.Items.Remove("Loading...");

                                                versionSelector.Items.Add(release.TagName);
                                            }));
                                        }
                                        catch (Exception e)
                                        {
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

                    if (failedToAdd.Count > 0)
                        UpdateStatus("Failed to add: " + String.Join(", ", failedToAdd));
                    else
                        UpdateStatus("Finished retrieving plugin versions...");
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
        private void closeApp(string logText)
        {
            if (_isClosing) return;
            Properties.Settings.Default.Save();

            _isClosing = true;

            dLogger.Log(logText);

            //Close all loggers
            FieldInfo[] fis = typeof(MainWindow).GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fis)
            {
                if (fieldInfo.FieldType == typeof(DebugLogger))
                {
                    var logger = fieldInfo.GetValue(this) as DebugLogger;
                    logger.Disable();
                }
            }

            fadeOut.Start();

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
            ControlManager.Home();
            if (tabControl.SelectedTab == homePage) return;

            tabControl.SelectedTab = homePage;
        }

        private void faqButton_Click(object sender, EventArgs e)
        {
            ControlManager.FAQ();
            if (tabControl.SelectedTab == faqPage) return;

            tabControl.SelectedTab = faqPage;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            ControlManager.Settings();
            if (tabControl.SelectedTab == settingsPage) return;

            tabControl.SelectedTab = settingsPage;
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
            RTL = 15,
            DoubleEdge = 35,
            Center = 15
        }

        public async void ClearStatus(CancellationToken cancelToken = default(CancellationToken))
        {
            int delay = 3000;
            StatusAnimationType animationType = StatusAnimationType.DoubleEdge;
            int textDelay = 15;

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

        private async void installButton_Click(object sender, EventArgs e)
        {
            installButton.Enabled = false;
            uninstallButton.Enabled = false;

            bool obsCompat = OBS.IsCompatible();
            bool winCompat = Windows.IsCompatible();

            if (!obsCompat || !winCompat)
            {
                installButton.Enabled = true;
                uninstallButton.Enabled = true;
                return;
            }

            await CaptureAudio.Install(versionSelector.GetItemText(versionSelector.SelectedItem));
            bool isInstalled = CaptureAudio.IsInstalled();

            installButton.Enabled = true;
            uninstallButton.Enabled = true;
            installButton.Text = isInstalled ? "Reinstall" : "Install";
            uninstallButton.Visible = isInstalled;

            ControlManager.Home();
        }

        private void obsInstallMethodSelector_SelectionChangeCommitted(object s, EventArgs e)
        {
            Guna2ComboBox sender = s as Guna2ComboBox;

            /* CUSTOM */
            if (sender.SelectedItem.ToString().ToLower() == "custom")
            {
                BetterFolderBrowser obsLoc = new BetterFolderBrowser
                {
                    Title = "Choose your OBS install location (Root folder)!",
                    RootFolder = @"C:\Program Files",
                    Multiselect = false
                };

                if (obsLoc.ShowDialog() == DialogResult.OK)
                {
                    string formattedOBSFolder = OBS.FormatFolder(obsLoc.SelectedFolder);
                    if (OBS.IsOBSFolder(formattedOBSFolder))
                    {
                        Properties.Settings.Default.OBSInstall = formattedOBSFolder;
                        Notify.Toast("OBS Location", "Succesfully changed OBS install location to:\n" + formattedOBSFolder, 2);
                    }
                    else
                    {
                        Notify.Toast("OBS Location", "Failed to update OBS studio install location...", 2);
                    }
                }
            }
            else
            {
                Properties.Settings.Default.OBSInstall = sender.SelectedItem.ToString().ToLower();
            }

            ControlManager.Home();
            ControlManager.Settings();
        }

        private void versionSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (versionSelector.GetItemText(versionSelector.SelectedItem) == "Loading...")
            {
                versionSelector.SelectedIndex = -1;
                return;
            }

            int installedIndex = versionSelector.Items.IndexOf(CaptureAudio.InstalledVersion());

            if (versionSelector.SelectedIndex < installedIndex) installButton.Text = "Update";
            else if (versionSelector.SelectedIndex == installedIndex) installButton.Text = "Reinstall";
            else if (versionSelector.SelectedIndex > installedIndex) installButton.Text = "Downgrade ";
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

        private async void UninstallButton_Click(object sender, EventArgs e)
        {
            UpdateStatus("Uninstalling the plugin...");
            await CaptureAudio.Uninstall();

            bool isInstalled = CaptureAudio.IsInstalled();

            installButton.Text = isInstalled ? "Reinstall" : "Install";
            uninstallButton.Visible = isInstalled;

            ControlManager.Home();
        }

        private void helpVideoButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=a_gYy27eTTw");
        }

    }
}
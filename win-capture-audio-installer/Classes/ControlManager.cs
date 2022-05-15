using System.IO;
using System.Reflection;
using System.Windows.Forms;
using win_capture_audio_installer.Information;

namespace win_capture_audio_installer.Classes
{
    public class ControlManager
    {
        static MainWindow MAIN = MainWindow.INSTANCE;
        public static void Home()
        {
            bool isOBSFolder = OBS.IsOBSFolder(Properties.Settings.Default.OBSInstall);

            if (!isOBSFolder) Properties.Settings.Default.OBSInstall = "windows";

            // Used to update information in bottom left in initialize
            OBS.GetVersion();
            Windows.GetBuildVersion();
            CaptureAudio.InstalledVersion();


            bool isInstalled = CaptureAudio.IsInstalled();
            MAIN.installButton.Text = isInstalled ? "Reinstall" : "Install";
            MAIN.uninstallButton.Visible = isInstalled;

        }

        public static void FAQ()
        {
            if (File.Exists(@"C:\temp\win-capture-audio-installer\Data\FAQ.rtf"))
            {

                MAIN.faqText.LoadFile(@"C:\temp\win-capture-audio-installer\Data\FAQ.rtf");
            }
            else
            {
                MAIN.faqText.Text = "Failed to load FAQ";
            }

        }

        public static void Settings()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MAIN.installerBuildLabel.Text = "Installer Build: " + version.Remove(version.Length - 2);

            if (Properties.Settings.Default.OBSInstall != "windows" && Properties.Settings.Default.OBSInstall != "steam")
            {
                MAIN.obsInstallMethodSelector.SelectedItem = "Custom";
            } else
            MAIN.obsInstallMethodSelector.SelectedItem = char.ToUpper(Properties.Settings.Default.OBSInstall[0]) + Properties.Settings.Default.OBSInstall.Substring(1);
        }
    }
}

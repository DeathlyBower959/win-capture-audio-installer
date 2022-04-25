using System.IO;
using win_capture_audio_installer.Information;

namespace win_capture_audio_installer.Classes
{
    public class ControlManager
    {
        static MainWindow MAIN = MainWindow.INSTANCE;
        public static void Home()
        {
            // Used to update information in bottom left in initialize
            OBS.GetVersion();
            Windows.GetBuildVersion();

            bool isOBSFolder = OBS.IsOBSFolder(Properties.Settings.Default.OBSInstall);

            MAIN.obsInstallLocationSelector.Checked = isOBSFolder;

            if (!isOBSFolder) Properties.Settings.Default.OBSInstall = "auto";
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

        }
    }
}

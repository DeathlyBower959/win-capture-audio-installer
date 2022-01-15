using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using win_capture_audio_installer.Information;

namespace win_capture_audio_installer.Classes
{
    public class CaptureAudio
    {
        static MainWindow MAIN = MainWindow.INSTANCE;
        /// <summary>
        /// Uninstalls the plugin
        /// </summary>
        public static void Uninstall()
        {
            string obsLoc = OBS.FindOBSInstallLoc();


            if (!Directory.Exists(obsLoc))
            {
                MAIN.dLogger.Log("Failed to uninstall: OBS Location Not Found", LogLevel.Error);
                return;
            }

            if (File.Exists(Path.Combine(obsLoc, "obs-plugins\\64bit\\win-capture-audio.dll")))
            {
                File.Delete(Path.Combine(obsLoc, "obs-plugins\\64bit\\win-capture-audio.dll"));
            }

            if (File.Exists(Path.Combine(obsLoc, "obs-plugins\\64bit\\win-capture-audio.pdb")))
            {
                File.Delete(Path.Combine(obsLoc, "obs-plugins\\64bit\\win-capture-audio.pdb"));
            }

            if (Directory.Exists(Path.Combine(obsLoc, "data\\obs-plugins\\win-capture-audio")))
            {
                Directory.Delete(Path.Combine(obsLoc, "data\\obs-plugins\\win-capture-audio"), true);
            }
        }

        /// <summary>
        /// Checks if all files required are present
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsInstalled()
        {
            string obsLoc = OBS.FindOBSInstallLoc();

            if (!Directory.Exists(obsLoc))
            {
                MAIN.dLogger.Log("Failed to validate if installed: OBS Location Not Found", LogLevel.Error);
                return false;
            }

            if (!Directory.Exists(Path.Combine(obsLoc, "obs-plugins\\64bit")))
            {
                MAIN.dLogger.Log("Failed to validate if installed: OBS Plugins Not Found", LogLevel.Error);
                return false;
            }

            if (!Directory.Exists(Path.Combine(obsLoc, "data\\obs-plugins")))
            {
                MAIN.dLogger.Log("Failed to validate if installed: OBS Plugin Data Not Found", LogLevel.Error);
                return false;
            }

            if (!Directory.Exists(Path.Combine(obsLoc, "data\\obs-plugins\\win-capture-audio")))
            {
                MAIN.dLogger.Log("Not Installed: Plugin Data not found", LogLevel.Error);
                return false;
            }

            if (!File.Exists(Path.Combine(obsLoc, "obs-plugins\\64bit\\win-capture-audio.dll")) || !File.Exists(Path.Combine(obsLoc, "obs-plugins\\64bit\\win-capture-audio.pdb")))
            {
                MAIN.dLogger.Log("Not Installed: Plugin DLL not found", LogLevel.Error);
                return false;
            }

            MAIN.dLogger.Log("Plugin is installed");

            return true;

        }

        /// <summary>
        /// Gets the version names of all the downloaded .zip's (Data\versions)
        /// </summary>
        /// <returns>string[]</returns>
        public static List<string> GetDownloadedVersions()
        {
            List<string> versions = new List<string>();

            foreach (FileInfo fileZip in new DirectoryInfo(@"C:\temp\win-capture-audio-installer\Data\versions").GetFiles())
            {
                int fileExtPos = fileZip.Name.LastIndexOf(".");
                if (fileExtPos >= 0)
                    versions.Add(fileZip.Name.Substring(0, fileExtPos));
            }

            return versions;
        }

        public static string InstalledVersion()
        {
            string obsLoc = OBS.FindOBSInstallLoc();

            if (obsLoc == null)
            {
                MAIN.dLogger.Log("OBS install location not found", LogLevel.Error);
                return null;
            }

            if (File.Exists(Path.Combine(obsLoc, @"obs-plugins\64bit\win-capture-audio-version.txt")))
            {
                string wincapversion = File.ReadAllText(Path.Combine(obsLoc, @"obs-plugins\64bit\win-capture-audio-version.txt"));
                MAIN.dLogger.Log("Plugin Version Found: " + wincapversion.Trim());
                return wincapversion.Trim();
            }
            else
            {

                return null;
            }
        }

        public static bool IsLatestVersion()
        {
            List<string> versions = GetDownloadedVersions();
            string latestVersion = versions[versions.Count - 1];

            string currentVersion = InstalledVersion();

            if (currentVersion != latestVersion)
            {
                Notify.Toast("Update", $"There is a new update avaliable for the plugin!\nYour Version: {currentVersion}\nLatest Version: {latestVersion}");
                return false;
            }

            return true;
        }



        /// <summary>
        /// Installs a specified version of the plugin (Based on the tag name on the github repo)
        /// </summary>
        /// <param name="versionTag">
        /// <para>Plugin Version</para>
        /// <para>Example: v2.1.0-beta</para>
        /// </param>
        public static void Install(string versionTag = null)
        {
            try
            {
                List<string> versions = (versionTag != null || versionTag.Trim() == string.Empty) ? (new List<string>(new string[1] { versionTag })) : GetDownloadedVersions();

                string latestVersion = versions[versions.Count - 1];

                string obsLoc = OBS.FindOBSInstallLoc();

                string file = Path.Combine(@"C:\temp\win-capture-audio-installer\Data\versions", latestVersion + ".zip");
                if (!File.Exists(file))
                {
                    MAIN.dLogger.Log("Version Not Found: " + latestVersion, LogLevel.Error);
                    Notify.Toast("Failed to install version: " + latestVersion, "The file was not found! Maybe check your internet?");
                    return;
                }

                Uninstall();

                if (obsLoc == null)
                {
                    MAIN.dLogger.Log("OBS install location not found", LogLevel.Error);
                    Notify.Toast("OBS not found", "I could not find the install location of OBS! Head over to the settings page, and make sure the location is correct.");
                    return;
                }

                ZipFile.ExtractToDirectory(Path.Combine(@"C:\temp\win-capture-audio-installer\Data\versions", latestVersion + ".zip"), obsLoc);

                File.WriteAllText(Path.Combine(obsLoc, @"obs-plugins\64bit\win-capture-audio-version.txt"), latestVersion);
                Notify.Toast("Installed!", $"Version {latestVersion} was successfully installed!", 2);
            }

            catch (Exception e)
            {
                MAIN.dLogger.Log(e);
                Notify.Toast("Failed to install", e.Message);
            }
        }
    }
}

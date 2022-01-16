using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows;
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
            string latestVersion = MAIN.versionsList[MAIN.versionsList.Count - 1].tag;

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
        public static async void Install(string versionTag = null)
        {
            if (MAIN.versionsList.Count == 0) return;
            try
            {
                Process[] obsInstances = Process.GetProcessesByName("obs64");
                if (obsInstances.Length > 0)
                {
                    if (MessageBox.Show("Would you like me to close OBS?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var process in obsInstances)
                        {
                            process.Kill();
                            process.WaitForExit();
                        }
                    }
                    else
                    {
                        MAIN.dLogger.Log("Canceled installing version, OBS was open...");
                        return;
                    }
                }

                PluginVersion latestVersion = (versionTag != null || versionTag.Trim() == string.Empty) ? MAIN.versionsList.Find(x => x.tag == versionTag) : MAIN.versionsList[MAIN.versionsList.Count - 1];

                if (latestVersion.tag == null || latestVersion.downloadURL == null)
                {
                    MAIN.dLogger.Log($"Plugin version {versionTag} was not found!", LogLevel.Error);
                    Notify.Toast("Plugin Version", "Failed to find that version!");
                    return;
                }

                string obsLoc = OBS.FindOBSInstallLoc();

                string file = @"C:\temp\win-capture-audio-installer\Data\plugin.zip";

                if (File.Exists(file))
                    File.Delete(file);

                Uninstall();

                if (obsLoc == null)
                {
                    MAIN.dLogger.Log("OBS install location not found", LogLevel.Error);
                    Notify.Toast("OBS not found", "I could not find the install location of OBS! Head over to the settings page, and make sure the location is correct.");
                    return;
                }

                MAIN.UpdateStatus($"Downloading version: {latestVersion.tag}...");
                await DownloadManager.DownloadAsync(
               latestVersion.downloadURL,
               @"C:\temp\win-capture-audio-installer\Data",
               "plugin.zip");

                if (!File.Exists(file))
                {
                    MAIN.ClearStatus();
                    MAIN.dLogger.Log("Version Not Found: " + latestVersion.tag, LogLevel.Error);
                    Notify.Toast("Failed to install version: " + latestVersion.tag, "The file was not found! Maybe check your internet?");
                    return;
                }

                MAIN.UpdateStatus($"Installing version: {latestVersion.tag}!");
                ZipFile.ExtractToDirectory(file, obsLoc);

                File.WriteAllText(Path.Combine(obsLoc, @"obs-plugins\64bit\win-capture-audio-version.txt"), latestVersion.tag);
                MAIN.UpdateStatus($"Installed version: {latestVersion.tag}!");

                Task.Run(() =>
                {
                    Notify.Toast("Installed!", $"Version {latestVersion.tag} was successfully installed!", 2);
                });


                if (File.Exists(file))
                    File.Delete(file);

                string obsPath = Path.Combine(obsLoc, @"bin\64bit\");
                if (File.Exists(obsPath + "obs64.exe"))
                {
                    var res = MessageBox.Show("Would you like me to open OBS?", "Are you sure?", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        Powershell.Invoke($"cd \"{obsPath}\"", "start obs64.exe");
                    }
                }
            }

            catch (Exception e)
            {
                MAIN.dLogger.Log(e);
                MAIN.ClearStatus();
                Notify.Toast("Failed to install", e.Message);
            }
        }
    }
}

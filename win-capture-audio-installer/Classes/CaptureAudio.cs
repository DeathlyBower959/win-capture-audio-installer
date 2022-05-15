using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;
using win_capture_audio_installer.Information;

namespace win_capture_audio_installer.Classes
{
    public class CaptureAudio
    {
        static MainWindow MAIN = MainWindow.INSTANCE;
        
        /// <summary>
        /// Uninstalls the plugin
        /// </summary>
        public static async Task Uninstall()
        {
            await Task.Run(() =>
            {
                Process[] obsInstances = Process.GetProcessesByName("obs{{ARCH}}".FormatArch());
                if (obsInstances.Length > 0)
                {
                    if (MessageBox.Show(MAIN, "Would you like me to close OBS?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (var process in obsInstances)
                        {
                            process.Kill();
                            process.WaitForExit();
                        }
                    }
                    else
                    {
                        MAIN.dLogger.Log("Canceled uninstalling version, OBS was open...");
                        return;
                    }
                }

                string obsLoc = OBS.FindOBSInstallLoc();

                if (!Directory.Exists(obsLoc))
                {
                    MAIN.dLogger.Log("Failed to uninstall: OBS Location Not Found", LogLevel.Error);
                    /*Notify.Toast("Uninstall Plugin", "I failed to uninstall the plugin!");*/
                    return;
                }


                if (File.Exists(Path.Combine(obsLoc, "obs-plugins\\{{ARCHBIT}}\\win-capture-audio.dll".FormatArch())))
                {
                    File.Delete(Path.Combine(obsLoc, "obs-plugins\\{{ARCHBIT}}\\win-capture-audio.dll".FormatArch()));
                }

                if (File.Exists(Path.Combine(obsLoc, "obs-plugins\\{{ARCHBIT}}\\win-capture-audio.pdb".FormatArch())))
                {
                    File.Delete(Path.Combine(obsLoc, "obs-plugins\\{{ARCHBIT}}\\win-capture-audio.pdb".FormatArch()));
                }

                if (File.Exists(Path.Combine(obsLoc, "obs-plugins\\{{ARCHBIT}}\\win-capture-audio-version.txt".FormatArch())))
                {
                    File.Delete(Path.Combine(obsLoc, "obs-plugins\\{{ARCHBIT}}\\win-capture-audio-version.txt".FormatArch()));
                }

                if (Directory.Exists(Path.Combine(obsLoc, "data\\obs-plugins\\win-capture-audio".FormatArch())))
                {
                    Directory.Delete(Path.Combine(obsLoc, "data\\obs-plugins\\win-capture-audio".FormatArch()), true);
                }

                MAIN.UpdateStatus("Plugin Uninstalled!");
            });
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

            if (!Directory.Exists(Path.Combine(obsLoc, "obs-plugins\\{{ARCHBIT}}".FormatArch())))
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


            if (!File.Exists(Path.Combine(obsLoc, "obs-plugins\\{{ARCHBIT}}\\win-capture-audio.dll".FormatArch())) || !File.Exists(Path.Combine(obsLoc, "obs-plugins\\{{ARCHBIT}}\\win-capture-audio.pdb".FormatArch())))
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

            if (File.Exists(Path.Combine(obsLoc, @"obs-plugins\{{ARCHBIT}}\win-capture-audio-version.txt".FormatArch())))
            {
                string wincapversion = File.ReadAllText(Path.Combine(obsLoc, @"obs-plugins\{{ARCHBIT}}\win-capture-audio-version.txt".FormatArch())).Trim();
                MAIN.dLogger.Log("Plugin Version Found: " + wincapversion);

                string[] currentText = MAIN.versions.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                currentText[2] = $"Plugin: {wincapversion}";
                MAIN.versions.Text = string.Join(Environment.NewLine, currentText);
                return wincapversion;
            }
            else
            {
                string[] currentText = MAIN.versions.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                currentText[2] = $"Plugin: Uninstalled";
                MAIN.versions.Text = string.Join(Environment.NewLine, currentText);
                return "";
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
        public static async Task Install(string versionTag = null)
        {
            if (MAIN.versionsList.Count == 0) return;
            try
            {
                Process[] obsInstances = Process.GetProcessesByName("obs{{ARCH}}".FormatArch());
                if (obsInstances.Length > 0)
                {
                    if (MessageBox.Show(MAIN, "Would you like me to close OBS?", "Are you sure?", MessageBoxButtons.YesNo)  == DialogResult.Yes)
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
                    /*Notify.Toast("Plugin Version", "Failed to find that version!");*/
                    return;
                }

                string obsLoc = OBS.FindOBSInstallLoc();

                string file = @"C:\temp\win-capture-audio-installer\Data\plugin.zip";

                if (File.Exists(file))
                    File.Delete(file);

                if (obsLoc == null)
                {
                    MAIN.dLogger.Log("OBS install location not found", LogLevel.Error);
                    MAIN.UpdateStatus("I failed to find the install location of OBS! Try heading over to the settings page, and make sure that the location is correct!");
                    /*Notify.Toast("OBS not found", "I could not find the install location of OBS! Head over to the settings page, and make sure the location is correct.");*/
                    return;
                }

                await Uninstall();

                MAIN.UpdateStatus($"Downloading version: {latestVersion.tag}...");
                await DownloadManager.DownloadAsync(
               latestVersion.downloadURL,
               @"C:\temp\win-capture-audio-installer\Data",
               "plugin.zip");

                if (!File.Exists(file))
                {
                    MAIN.ClearStatus();
                    MAIN.dLogger.Log("Version Not Found: " + latestVersion.tag, LogLevel.Error);
                    MAIN.UpdateStatus("The version was not found! Maybe check your internet?");
                    /*Notify.Toast("Failed to install version: " + latestVersion.tag, "The file was not found! Maybe check your internet?");*/
                    return;
                }

                MAIN.UpdateStatus($"Installing version: {latestVersion.tag}!");
                ZipFile.ExtractToDirectory(file, obsLoc);

                File.WriteAllText(Path.Combine(obsLoc, @"obs-plugins\{{ARCHBIT}}\win-capture-audio-version.txt".FormatArch()), latestVersion.tag);
                MAIN.UpdateStatus($"Installed version: {latestVersion.tag}!");

                Notify.Toast("Installed!", $"Version {latestVersion.tag} was successfully installed!", 2);


                if (File.Exists(file))
                    File.Delete(file);

                string obsBinPath = Path.Combine(obsLoc, @"bin\{{ARCHBIT}}\".FormatArch());
                if (File.Exists(obsBinPath + "obs{{ARCH}}.exe".FormatArch()))
                {
                    if (MessageBox.Show(MAIN, "Would you like me to open OBS?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Powershell.Invoke($"cd \"{obsBinPath}\"", "start obs{{ARCH}}.exe".FormatArch());
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

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using win_capture_audio_installer.Classes;

namespace win_capture_audio_installer.Information
{
    public static class OBS
    {
        static MainWindow MAIN = MainWindow.INSTANCE;

        static string STEAM_OBS_APPID = "1905180";
        static string DEFAULT_OBS_LOCATION = @"C:\Program Files\obs-studio";

        public static bool IsCompatible()
        {
            Version obsVersion = GetVersion();

            if (obsVersion == null)
            {
                MAIN.dLogger.Log("OBS version not found!");
                Notify.Toast("OBS Version", "I couldnt find your version of OBS! Check that it is installed!", 3);

                return false;
            }

            if (obsVersion < MAIN.minOBSVersion)
            {
                MAIN.dLogger.Log("OBS Version unsupported, please update!", LogLevel.Error);
                Notify.Toast("OBS Version", $"Your version of OBS is not supported. You currently have version {obsVersion}. Please update to higher than version {MAIN.minOBSVersion}!");
                return false;
            }

            return true;
        }
        public static bool IsOBSFolder(string path)
        {
            string[] required = new string[] {
                "bin\\{{WINBIT}}bit",
                "bin\\{{WINBIT}}bit\\obs{{WINBIT}}.exe",
                "data",
                "obs-plugins"
            };

            foreach (string i in required)
            {
                if (!Directory.Exists(Path.Combine(path, i)) && !File.Exists(Path.Combine(path, i.Replace("{{WINBIT}}", Environment.Is64BitOperatingSystem ? "64": "32")))) return false;
            }

            return true;
        }
        public static string FindOBSInstallLoc()
        {
            if (Properties.Settings.Default.OBSInstall != "auto" && Directory.Exists(Properties.Settings.Default.OBSInstall))
            {
                if (IsOBSFolder(Properties.Settings.Default.OBSInstall))
                    return Properties.Settings.Default.OBSInstall;
                else
                {
                    Properties.Settings.Default.OBSInstall = "auto";
                    Notify.Toast("OBS Install", "OBS install location is not valid, it has been reset to automatically search for!");
                }
            }

            if (IsOBSFolder(DEFAULT_OBS_LOCATION)) return DEFAULT_OBS_LOCATION;

            string installLocation = WindowsCurrentInstall();
            if (installLocation != null) return installLocation;
            
            installLocation = WOWNode();
            if (installLocation != null) return installLocation;
            
            installLocation = SteamApp();
            if (installLocation != null) return installLocation;

            return "";
        }

        private static string WOWNode()
        {
            MAIN.dLogger.Log("Searching WOW6432Node for OBS install location");

            RegistryKey lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey parentKey = lm.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall");

            string[] nameList = parentKey.GetSubKeyNames();
            for (int i = 0; i < nameList.Length; i++)
            {
                RegistryKey regKey = parentKey.OpenSubKey(nameList[i]);
                try
                {
                    if (nameList[i].ToLower() == "obs studio")
                    {
                        if (regKey.GetValue("UninstallString") != null)
                        {
                            string installLoc = Path.GetDirectoryName(regKey.GetValue("UninstallString").ToString());
                            MAIN.dLogger.Log("WOW6432Node | OBS Install Loc: " + installLoc, LogLevel.Success);
                            return installLoc;
                        }
                    }
                }
                catch { }
            }
            MAIN.dLogger.Log("Failed to find OBS install location using WOW6432Node", LogLevel.Error);


            return null;
        }

        private static string SteamApp()
        {
            MAIN.dLogger.Log("Searching Steam App for OBS install location");

            RegistryKey lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            RegistryKey steamParentKey = lm.OpenSubKey($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {STEAM_OBS_APPID}");
            string steamInstallLoc = steamParentKey.GetValue("InstallLocation").ToString();
            if (steamInstallLoc != null)
            {
                MAIN.dLogger.Log("Steam App | OBS Install Loc: " + steamInstallLoc, LogLevel.Success);

                return steamInstallLoc;
            }

            MAIN.dLogger.Log("Failed to find OBS install location using Steam App", LogLevel.Error);

            return null;
        }

        private static string WindowsCurrentInstall()
        {
            MAIN.dLogger.Log("Searching CurrentVersion\\Uninstall for OBS install location");
            RegistryKey lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey parentKey = lm.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");

            string[] nameList = parentKey.GetSubKeyNames();
            for (int i = 0; i < nameList.Length; i++)
            {
                RegistryKey regKey = parentKey.OpenSubKey(nameList[i]);
                try
                {
                    if ((regKey.GetValue("DisplayName") ?? "_)(@#_)(!@_#)(@!_#)(@!_#)(").ToString().ToLower().Contains("obs studio"))
                    {
                        if (regKey.GetValue("InstallLocation") != null)
                        {
                            string installLoc = regKey.GetValue("InstallLocation").ToString();
                            MAIN.dLogger.Log("CurrentVersion\\Uninstall | OBS Install Loc: " + installLoc, LogLevel.Success);

                            return installLoc;
                        }
                    }
                }
                catch { }
            }
            MAIN.dLogger.Log("Failed to find OBS install location using CurrentVersion\\Uninstall", LogLevel.Error);

            return null;

        }

        public static Version GetVersion()
        {
            string obsInstallLoc = FindOBSInstallLoc();

            var versionInfo = FileVersionInfo.GetVersionInfo(Path.Combine(obsInstallLoc, @"bin\64bit\obs64.exe"));
            string version = versionInfo.FileVersion;
            if (version != null)
            {
                string[] currentText = MAIN.versions.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                currentText[1] = $"OBS: {version}";
                MAIN.versions.Text = string.Join(Environment.NewLine, currentText);

                return new Version(version);
            }

            return null;
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using win_capture_audio_installer.Classes;

namespace win_capture_audio_installer.Information
{
    public static class OBS
    {
        static MainWindow MAIN = MainWindow.INSTANCE;

        static string STEAM_OBS_APPID = "1905180";

        public static bool IsCompatible()
        {
            Version obsVersion = GetVersion();

            if (obsVersion == null)
            {
                MAIN.dLogger.Log("OBS version not found!");
                Notify.Toast("OBS Version", "I couldnt find your version of OBS! Check that it is installed!", 3);

                return false;
            }

            Version minObsVersion = MAIN.versionsList[MAIN.versionSelector.SelectedIndex].MinOBSVersion;

            if (obsVersion < minObsVersion)
            {
                MAIN.dLogger.Log("OBS Version unsupported, please update!", LogLevel.Error);
                Notify.Toast("OBS Version", $"This plugin version doesn't support your OBS version. You currently have version {obsVersion}. Please update to version higher than {minObsVersion}!");
                return false;
            }

            return true;
        }

        public static string FormatFolder(string path)
        {
            List<string> paths = path.Split('\\', '/').ToList();

            int pathIndex = paths.FindIndex((item) =>
            {
                return item.ToLower().Contains("obs") && item.ToLower().Contains("studio");
            });


            if (pathIndex > -1)
            {
                return String.Join("\\", paths.Take(pathIndex + 1));
            }

            return path;
        }

        public static bool IsOBSFolder(string p)
        {
            string path = p;
            if (p == "windows" || p == "steam")
            {
                path = FindOBSInstallLoc();
            }
            if (path == null || path == string.Empty) return false;

            string[] required = new string[] {
                "bin\\{{ARCHBIT}}",
                "bin\\{{ARCHBIT}}\\obs{{ARCH}}.exe",
                "data",
                "obs-plugins"
            };

            foreach (string i in required)
            {
                if (!Directory.Exists(Path.Combine(path, i.FormatArch())) && !File.Exists(Path.Combine(path, i.FormatArch()))) return false;
            }

            return true;
        }
        public static string FindOBSInstallLoc()
        {
            if (Properties.Settings.Default.OBSInstall != "windows" && Properties.Settings.Default.OBSInstall != "steam" && Directory.Exists(Properties.Settings.Default.OBSInstall))
            {
                if (IsOBSFolder(Properties.Settings.Default.OBSInstall))
                    return Properties.Settings.Default.OBSInstall;
                else
                {
                    Properties.Settings.Default.OBSInstall = "windows";
                    Notify.Toast("OBS Install", "OBS install location is not valid, it has been reset to automatically search for!");
                }
            }


            string installLocation = null;

            if (Properties.Settings.Default.OBSInstall == "windows")
            {
                installLocation = WOWNode();
                if (installLocation == null)
                    installLocation = WindowsCurrentInstall();
            }
            else if (Properties.Settings.Default.OBSInstall == "steam")
            {
                installLocation = SteamApp();
            }

            if (installLocation != null) return installLocation;

            return "";
        }

        private static string WOWNode()
        {
            if (!Environment.Is64BitOperatingSystem)
            {
                MAIN.dLogger.Log("Skipping searching WOW6432Node for OBS, as 32bit system");
                return null;
            }
            MAIN.dLogger.Log("Searching WOW6432Node for OBS install location");

            RegistryKey lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
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

            RegistryKey lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            RegistryKey steamParentKey = lm.OpenSubKey($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {STEAM_OBS_APPID}");
            string steamInstallLoc = steamParentKey?.GetValue("InstallLocation")?.ToString();
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
            RegistryKey lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
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

            if (obsInstallLoc == null || obsInstallLoc == String.Empty) return null;

            var versionInfo = FileVersionInfo.GetVersionInfo(Path.Combine(obsInstallLoc, @"bin\{{ARCHBIT}}\obs{{ARCH}}.exe".FormatArch()));
            string version = versionInfo.FileVersion;

            string[] currentText = MAIN.versions.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (version != null)
            {
                currentText[1] = $"OBS: {version}";
                MAIN.versions.Text = string.Join(Environment.NewLine, currentText);

                return new Version(version);
            }

            currentText[1] = $"OBS: ?";
            MAIN.versions.Text = string.Join(Environment.NewLine, currentText);

            return null;
        }
    }
}

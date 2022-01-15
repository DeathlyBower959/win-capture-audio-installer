using Microsoft.Win32;
using System;
using System.IO;
using win_capture_audio_installer.Classes;

namespace win_capture_audio_installer.Information
{
    public static class OBS
    {
        static MainWindow MAIN = MainWindow.INSTANCE;

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
                "bin\\64bit",
                "bin\\64bit\\obs64.exe",
                "data",
                "obs-plugins"
            };

            foreach (string i in required)
            {
                if (!Directory.Exists(Path.Combine(path, i)) && !File.Exists(Path.Combine(path, i))) return false;
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
                    Notify.Toast("OBS Install", "OBS install location is not valid, it has been reset to automatically find!");
                }
            }

            var currentInstall = windowsCurrentInstall();
            if (currentInstall != null) return currentInstall;
            var wownode = WOWNode();
            if (wownode != null) return wownode;

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

        private static string windowsCurrentInstall()
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
            MAIN.dLogger.Log("Searching WOW6432Node for OBS version");

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
                        if (regKey.GetValue("DisplayVersion") != null)
                        {
                            string version = regKey.GetValue("DisplayVersion").ToString();
                            MAIN.dLogger.Log("WOW6432Node | OBS Version: " + version, LogLevel.Success);

                            string[] currentText = MAIN.versions.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                            currentText[1] = $"OBS: {version}";
                            MAIN.versions.Text = string.Join(Environment.NewLine, currentText);

                            return new Version(version);
                        }
                    }
                }
                catch { }
            }
            MAIN.dLogger.Log("Failed to find OBS version using WOW6432Node", LogLevel.Error);
            return null;
        }
    }
}

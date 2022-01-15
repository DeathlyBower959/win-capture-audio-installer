using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using win_capture_audio_installer.Classes;

namespace win_capture_audio_installer.Information
{
    public static class Windows
    {
        static MainWindow MAIN = MainWindow.INSTANCE;

        public static bool IsCompatible()
        {
            int buildVersion = int.Parse(GetBuildVersion());

            if (buildVersion < MAIN.minWINVersion)
            {
                MAIN.dLogger.Log("Windows version unsupported, please update!", LogLevel.Error);
                Notify.Toast("Windows Version", $"Your version of Windows is not supported. You currently have build version {buildVersion}. Please update to higher than build version {MAIN.minWINVersion}!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns the OS Build of windows 
        /// (winver > Version > OS Build [Truncated])
        /// </summary>
        /// <returns>String</returns>
        public static string GetBuildVersion()
        {
            MAIN.dLogger.Log("Searching Windows NT for Current Windows Build Version");

            RegistryKey lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey regKey = lm.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            try
            {
                if (regKey.GetValue("CurrentBuild") != null)
                {
                    string version = regKey.GetValue("CurrentBuild").ToString();
                    MAIN.dLogger.Log("Current Build: " + version, LogLevel.Success);

                    string[] currentText = MAIN.versions.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    currentText[0] = $"Win Build: {version}";
                    MAIN.versions.Text = string.Join(Environment.NewLine, currentText);

                    return version;
                }
            }
            catch (Exception e)
            {
                MAIN.dLogger.Log("Failed to find Current Windows Build Version", LogLevel.Error);
                MAIN.dLogger.Log(e);
            }
            return null;
        }

        /// <summary>
        /// Example 21H1
        /// </summary>
        /// <returns>String</returns>
        public static string DisplayVersion()
        {
            MAIN.dLogger.Log("Searching Windows NT for Windows Version");

            RegistryKey lm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey regKey = lm.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            try
            {
                if (regKey.GetValue("DisplayVersion") != null)
                {
                    string version = regKey.GetValue("DisplayVersion").ToString();
                    MAIN.dLogger.Log("Display Version: " + version, LogLevel.Success);
                    return version;
                }
            }
            catch (Exception e)
            {
                MAIN.dLogger.Log("Failed to find Windows Display Version", LogLevel.Error);
                MAIN.dLogger.Log(e);
            }
            return null;
        }
    }
}

using System;

namespace win_capture_audio_installer.Classes
{
    public class PluginVersion
    {
        public PluginVersion(string downloadURL, string tag, Version minOBSVersion)
        {
            DownloadURL = downloadURL;
            Tag = tag;
            MinOBSVersion = minOBSVersion;
        }

        public string DownloadURL;
        public string Tag;
        public Version MinOBSVersion;
    }
}

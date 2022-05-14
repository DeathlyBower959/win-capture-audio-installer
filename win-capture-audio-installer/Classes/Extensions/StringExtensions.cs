using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace win_capture_audio_installer
{
    public static class StringExtensions
    {
        public static String FormatArch(this String s)
        {
            return s.Replace("{{ARCH}}", Environment.Is64BitOperatingSystem ? "64" : "32").Replace("{{ARCHBIT}}", Environment.Is64BitOperatingSystem ? "64bit" : "32bit");
        }
    }
}

using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using win_capture_audio_installer.Classes;

namespace win_capture_audio_installer
{
    public static class InternetManager
    {
        static MainWindow MAIN = MainWindow.INSTANCE;

        [DllImport("wininet.dll")]
        static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        static bool OnlineCheck()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://github.com/");
            request.Timeout = 15000;
            request.Method = "HEAD";
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        public static bool InternetCheck()
        {
            bool internetcheckone = InternetGetConnectedState(out int _, 0);
            bool internetchecktwo = OnlineCheck();

            if (internetcheckone == true && internetchecktwo == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<bool> PREVIOUS_INTERNET_STATE = new List<bool>() { true, true };

        public static void ReCheckInternet()
        {
            MAIN.internet = InternetGetConnectedState(out int _, 0);

            PREVIOUS_INTERNET_STATE.Add(MAIN.internet);
            PREVIOUS_INTERNET_STATE.RemoveAt(PREVIOUS_INTERNET_STATE.Count - 3);
            if (MAIN.internet && !PREVIOUS_INTERNET_STATE[PREVIOUS_INTERNET_STATE.Count - 2])
            {
                Notify.Toast("Internet Connection", "Internet Connection Found!", 2);
                MAIN.DownloadRequiredFiles();
            }
            else if (!MAIN.internet && PREVIOUS_INTERNET_STATE[PREVIOUS_INTERNET_STATE.Count - 2])
            {
                Notify.Toast("Internet Connection", "Internet Connection Lost ;(", 2);
                MAIN.UpdateStatus("Lost internet connection...");
            }
        }
    }
}

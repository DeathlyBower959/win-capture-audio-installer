using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using win_capture_audio_installer.Classes.Structs;

namespace win_capture_audio_installer.Classes
{
    public static class Web
    {
        static string ApiURL = "https://api.github.com/repos/bozbez/win-capture-audio/releases";
        public static List<GithubRelease.Root> GetReleases()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiURL);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.UserAgent = "win-capture-audio-installer";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusDescription == "OK")
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                List<GithubRelease.Root> resData = JsonConvert.DeserializeObject<List<GithubRelease.Root>>(responseFromServer);

                return resData;
            }
            else
            {
                return null;
            }

        }
    }
}

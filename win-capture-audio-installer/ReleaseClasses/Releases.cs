using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace win_capture_audio_installer.ReleaseClasses
{
    public class Releases
    {
        public List<Release> data { get; set; }
        public class Uploader
        {
            public string avatar_url { get; set; }
            public string events_url { get; set; }
            public string followers_url { get; set; }
            public string following_url { get; set; }
            public string gists_url { get; set; }
            public string gravatar_id { get; set; }
            public string html_url { get; set; }
            public int id { get; set; }
            public string login { get; set; }
            public string node_id { get; set; }
            public string organizations_url { get; set; }
            public string received_events_url { get; set; }
            public string repos_url { get; set; }
            public bool site_admin { get; set; }
            public string starred_url { get; set; }
            public string subscriptions_url { get; set; }
            public string type { get; set; }
            public string url { get; set; }
        }

        public class Asset
        {
            public string browser_download_url { get; set; }
            public string content_type { get; set; }
            public DateTime created_at { get; set; }
            public int download_count { get; set; }
            public int id { get; set; }
            public object label { get; set; }
            public string name { get; set; }
            public string node_id { get; set; }
            public int size { get; set; }
            public string state { get; set; }
            public DateTime updated_at { get; set; }
            public Uploader uploader { get; set; }
            public string url { get; set; }
        }

        public class Author
        {
            public string avatar_url { get; set; }
            public string events_url { get; set; }
            public string followers_url { get; set; }
            public string following_url { get; set; }
            public string gists_url { get; set; }
            public string gravatar_id { get; set; }
            public string html_url { get; set; }
            public int id { get; set; }
            public string login { get; set; }
            public string node_id { get; set; }
            public string organizations_url { get; set; }
            public string received_events_url { get; set; }
            public string repos_url { get; set; }
            public bool site_admin { get; set; }
            public string starred_url { get; set; }
            public string subscriptions_url { get; set; }
            public string type { get; set; }
            public string url { get; set; }
        }

        public class Reactions
        {
            [JsonProperty("+1")]
            public int _1 { get; set; }

            [JsonProperty("-1")]
            public int __1 { get; set; }
            public int confused { get; set; }
            public int eyes { get; set; }
            public int heart { get; set; }
            public int hooray { get; set; }
            public int laugh { get; set; }
            public int rocket { get; set; }
            public int total_count { get; set; }
            public string url { get; set; }
        }

        public class Release
        {
            public List<Asset> assets { get; set; }
            public string assets_url { get; set; }
            public Author author { get; set; }
            public string body { get; set; }
            public DateTime created_at { get; set; }
            public bool draft { get; set; }
            public string html_url { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string node_id { get; set; }
            public bool prerelease { get; set; }
            public DateTime published_at { get; set; }
            public Reactions reactions { get; set; }
            public string tag_name { get; set; }
            public string tarball_url { get; set; }
            public string target_commitish { get; set; }
            public string upload_url { get; set; }
            public string url { get; set; }
            public string zipball_url { get; set; }
        }


    }
}

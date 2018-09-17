using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Models
{
    class About_user
    {
        private string id;
        private string user_id;
        private string influences;
        private string video_link_1;
        private string sample_link_1;
        private string sample_link_2;
        private string about_me;
        private string prefered_genre;
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return id; } set { id = value; } }
        [JsonProperty(PropertyName = "user_id")]
        public string User_id { get { return user_id; } set { user_id = value; } }
        [JsonProperty(PropertyName = "influences")]
        public string Influences { get { return influences; } set { influences = value; } }
        [JsonProperty(PropertyName = "video_link_1")]
        public string Video_link_1 { get { return video_link_1; } set { video_link_1 = value; } }
        [JsonProperty(PropertyName = "video_link_2")]
        public string Sample_link_1 { get { return sample_link_1; } set { sample_link_1 = value; } }
        [JsonProperty(PropertyName = "video_link_3")]
        public string Sample_link_2 { get { return sample_link_2; } set { sample_link_2 = value; } }
        [JsonProperty(PropertyName = "about_me")]
        public string About_me { get { return about_me; } set { about_me = value; } }
        [JsonProperty(PropertyName = "preffered_genre")]
        public string Prefered_genre { get { return prefered_genre; } set { prefered_genre = value; } }
        [JsonProperty(PropertyName = "Version")]
        public string Version { get; set; }
        string location;
        [JsonProperty(PropertyName = "Location")]
        public string Location { get { return location; } set { location = value; } }
        /// <summary>
        /// Azure created at time stamp
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Azure UpdateAt timestamp for online/offline sync
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}

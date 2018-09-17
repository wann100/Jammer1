using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Models
{
    class User_match_setttings
    {/// <summary>
     /// general key
     /// </summary>
        string id;
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return id; } set { id = value; } }

        /// <summary>
        /// the user key for which this information is about
        /// </summary>
        private string user_id;
        [JsonProperty(PropertyName = "user_id")]
        public string User_id { get { return user_id; } set { user_id = value; } }

        private string instruments;
        [JsonProperty(PropertyName = "instruments")]
        public string Instruments { get { return instruments; } set { instruments = value; } }

        private string genres;
        [JsonProperty(PropertyName = "Genres")]
        public string Genres { get { return genres; } set { genres = value; } }



        private string maximumage;
        [JsonProperty(PropertyName = "maximumage")]
        public string Maximumage { get { return maximumage; } set { maximumage = value; } }

        private string minimumage;
        [JsonProperty(PropertyName = "minimumage")]
        public string Minimumage { get { return minimumage; } set { minimumage = value; } }

        private string use_my_location;
        [JsonProperty(PropertyName = "use_my_location")]
        public string Use_my_location { get { return use_my_location; } set { use_my_location = value; } }


        public string Version { get; set; }
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

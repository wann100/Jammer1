using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Models
{
    class Jam
    {/// <summary>
    /// general key
    /// </summary>
        string id;
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return id; } set { id = value; } }
        /// <summary>
        /// the user who is asking to jam.
        /// </summary>
        string user_asking_to_jam_id;
        [JsonProperty(PropertyName = "user_asking_to_jam")]
        public string User_asking_to_jam_id { get { return user_asking_to_jam_id; } set { user_asking_to_jam_id = value; } }

        /// <summary>
        /// the user that is asked to jam.
        /// </summary>
        string user_jamming_with_id;
        [JsonProperty(PropertyName = "user_jamming_with_id")]
        public string User_jamming_with_id { get { return user_jamming_with_id; } set { user_jamming_with_id = value; } }
        /// <summary>
        /// Gets or sets the date UTc(saves date and time.
        /// </summary>
        /// <value>The date .</value>
        public DateTime Date_and_time { get; set; }

        [JsonIgnore]
        public string DateDisplay { get { return Date_and_time.ToLocalTime().ToString("d"); } }

        [JsonIgnore]
        public string TimeDisplay { get { return Date_and_time.ToLocalTime().ToString("t"); } }
        public string AzureVersion { get; set; }
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

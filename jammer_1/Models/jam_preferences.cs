using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Models
{
    class Jam_preferences
    {
        /// <summary>
        /// General Key
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

        /// <summary>
        /// ex. Rythym vs Lead vs Soloing
        /// </summary>
        string specialty;
        [JsonProperty(PropertyName = "specialty")]
        public string Specialty { get { return specialty; } set { specialty = value; } }


        /// <summary>
        /// id of the instrument you prefer your jam buddy to have
        /// </summary>
        string pref_companion_instrument_type_id;
        [JsonProperty(PropertyName = "specialty")]
        public string Pref_companion_instrument_type_id { get { return pref_companion_instrument_type_id; } set { pref_companion_instrument_type_id = value; } }

        /// <summary>
        /// Jam type preference (Original Music vs covers)
        /// </summary>
        string jam_pref_type;
        [JsonProperty(PropertyName = "jam_pref_type")]
        public string Jam_pref_type { get { return jam_pref_type; } set { jam_pref_type = value; } }
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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Models
{
    class Availability
    {/// <summary>
     /// general key
     /// </summary>
        string id;
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return id; } set { id = value; } }

        /// <summary>
        /// id of the user which is connected to this availablity
        /// </summary>
        string user_id;
        [JsonProperty(PropertyName = "user_id")]
        public string User_id { get { return user_id; } set { user_id = value; } }
        /// <summary>
        /// availability for this day
        /// </summary>
        string monday_availability;
        [JsonProperty(PropertyName = "monday_availability")]
        public string Monday_availability { get { return monday_availability; } set { monday_availability = value; } }
        /// <summary>
        /// availability for this day
        /// </summary>
        string tuesday_availability;
        [JsonProperty(PropertyName = "tuesday_availability")]
        public string Tuesday_availability { get { return tuesday_availability; } set { tuesday_availability = value; } }
        /// <summary>
        /// availability for this day
        /// </summary>
        string wednesday_availability;
        [JsonProperty(PropertyName = "wednesday_availability")]
        public string Wednesday_availability { get { return wednesday_availability; } set { wednesday_availability = value; } }
        /// <summary>
        /// availability for this day
        /// </summary>
        string thursday_availability;
        [JsonProperty(PropertyName = "thursday_availability")]
        public string Thursday_availability { get { return thursday_availability; } set { thursday_availability = value; } }
        /// <summary>
        /// availability for this day
        /// </summary>
        string friday_availability;
        [JsonProperty(PropertyName = "friday_availability")]
        public string Friday_availability { get { return friday_availability; } set { friday_availability = value; } }
        /// <summary>
        /// availability for this day
        /// </summary>
        string saturday_availability;
        [JsonProperty(PropertyName = "saturday_availability")]
        public string Saturday_availability { get { return saturday_availability; } set { saturday_availability = value; } }
        /// <summary>
        /// availability for this day
        /// </summary>
        string sunday_availability;
        [JsonProperty(PropertyName = "sunday_availability")]
        public string Sunday_availability { get { return sunday_availability; } set { sunday_availability = value; } }
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

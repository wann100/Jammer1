using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Models
{
    class Jammer_rating
    {/// <summary>
    /// general key
    /// </summary>
        string id;
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return id; } set { id = value; } }
        /// <summary>
        /// id for user being rated
        /// </summary>
        string user_being_rated_id;
        [JsonProperty(PropertyName = "user_being_rated_id")]
        public string User_being_rated_id { get { return User_being_rated_id; } set { user_being_rated_id = value; } }
        /// <summary>
        /// id of user doing the rating
        /// </summary>
        string user_that_is_rating_id;
        [JsonProperty(PropertyName = "user_that_is_rating_id")]
        public string User_that_is_rating_id { get { return user_that_is_rating_id; } set { user_that_is_rating_id = value; } }
        /// <summary>
        /// rating given by user doing the rating
        /// </summary>
        string rating;
        [JsonProperty(PropertyName = "rating")]
        public string Rating { get { return rating; } set { rating = value; } }
        /// <summary>
        /// The jam session id that the rating user is rating
        /// </summary>
        string jam_id;
        [JsonProperty(PropertyName = "jam_id")]
        public string Jam_id { get { return jam_id; } set { jam_id = value; } }
        string jam_location_id;
        /// <summary>
        /// Link to location of the jam session
        /// </summary>
        [JsonProperty(PropertyName = "jam_location_id")]
        public string Jam_location_id { get { return jam_location_id; } set { jam_location_id = value; } }
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

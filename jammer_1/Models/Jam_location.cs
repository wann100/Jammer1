using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Models
{
    class Jam_location
    {
        /// <summary>
        /// General Key
        /// </summary>
        string id;
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return id; } set { id = value; } }

        /// <summary>
        /// the name of the location
        /// </summary>
        string location_name ;
        [JsonProperty(PropertyName = "location_name")]
        public string Location_name { get { return location_name; } set { location_name = value; } }
        /// <summary>
        /// address of jame location
        /// </summary>
        string address;
        [JsonProperty(PropertyName = "address")]
        public string Address { get { return address; } set { address = value; } }
        
        /// <summary>
        /// the type of location this is( Not sure what types are necessary)
        /// </summary>
        string location_type_id;
        [JsonProperty(PropertyName = "location_type_id")]
        public string Location_type_id { get { return location_type_id; } set { location_type_id = value; } }

        /// <summary>
        /// hours of operations for jam location
        /// </summary>
        string hours_of_operation;
        [JsonProperty(PropertyName = "hours_of_operation")]
        public string Hours_of_operation { get { return hours_of_operation; } set { hours_of_operation = value; } }

        /// <summary>
        /// rating for location
        /// </summary>
        string location_rating;
        [JsonProperty(PropertyName = "location_rating")]
        public string Location_rating { get { return location_rating; } set { location_rating = value; } }
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

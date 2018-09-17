using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jammer_1.Models
{

    public class User //: MobileServiceUser
    {
        string id;
        string email;
        string location;
        long jammer_rating;
        string phone_number;
        string facebook_id;
        string google_id;
        string first_name;
        string last_name;
        string b_day;
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return id; } set { id = value; } }
        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [JsonProperty(PropertyName = "location")]
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        [JsonProperty(PropertyName = "jammer_rating")]
        public long JammerRating
        {
            get { return jammer_rating; }
            set { jammer_rating = value; }
        }
        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber
        {
            get { return phone_number; }
            set { phone_number = value; }
        }
        [JsonProperty(PropertyName = "facebook_id")]
        public string FacebookId
        {
            get { return facebook_id; }
            set { facebook_id = value; }
        }
        [JsonProperty(PropertyName = "google_id")]
        public string GoogleId
        {
            get { return google_id; }
            set { google_id = value; }
        }
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName
        {
            get { return first_name; }
            set { first_name = value; }
        }
        [JsonProperty(PropertyName = "last_name")]
        public string LastName
        {
            get { return last_name; }
            set { last_name = value; }
        }
        [JsonProperty(PropertyName = "b_day")]
        public string B_day
        {
            get { return b_day; }
            set { b_day = value; }
        }

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

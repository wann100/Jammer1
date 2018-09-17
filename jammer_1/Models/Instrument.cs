using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Models
{
   public class Instrument// : IInstrument
    {
        /// <summary>
        /// General Key
        /// </summary>
       private string id;
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return id; } set { id = value; } }
    
        /// <summary>
        /// the user key for which this information is about
        /// </summary>
        public static List<string> list_of_genre = new List<string>()
        {
            "Indie","Rock","Blues","Jazz" };
        public static List<string> list_of_instruments = new List<string>()
        {
           "", "Guitar","Drums","Vocal","Piano","Bass","Trombone","Flute","Trumpet","Tuba","Clarinet","Saxophone","Other" };
        private string user_id;
        [JsonProperty(PropertyName = "user_id")]
        public string User_id { get { return user_id; } set { user_id = value; } }

        /// <summary>
        /// how long this user has been playing this instrument
        /// </summary>
        private DateTimeOffset time_playing_instrument;
        [JsonProperty(PropertyName = "months")]
        public DateTimeOffset Time_playing_instrument { get { return time_playing_instrument; } set { time_playing_instrument = value; } }
        /// <summary>
        /// this is the key to get what kind of instrument this is. for 
        /// example its name like a trombone. this comes from the instrument_type_list
        /// </summary>
        private string instrument_name;
        [JsonProperty(PropertyName = "Instrument_name")]
        public string Instrument_name { get { return instrument_name; } set { instrument_name = value; } }

        /// <summary>
        /// what kind of genre does the user play with this instrument
        /// </summary>
        private string genre_name;

        [JsonProperty(PropertyName = "genre_name")]
        public string Genre_name { get { return genre_name; } set { genre_name = value; } }

        /// <summary>
        /// users skill rating based off of algorithm
        /// </summary>
        private string skill_rating;
        [JsonProperty(PropertyName = "skill_rating")]
        public string Skill_rating { get { return skill_rating; } set { skill_rating = value; } }
        [JsonProperty(PropertyName = "Version")]
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

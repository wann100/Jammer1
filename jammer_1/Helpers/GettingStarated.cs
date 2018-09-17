using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Helpers
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class GettingStarted
    {
        [JsonProperty("greeting")]
        public string Greeting { get; set; }

        [JsonProperty("instructions")]
        public Instruction[][] Instructions { get; set; }
    }

    public partial class Instruction
    {
        [JsonProperty("expires_on")]
        public string ExpiresOn { get; set; }

        [JsonProperty("user_claims")]
        public UserClaim[] UserClaims { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("provider_name")]
        public string ProviderName { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }

    public partial class UserClaim
    {
        [JsonProperty("typ")]
        public string Typ { get; set; }

        [JsonProperty("val")]
        public string Val { get; set; }
    }


    public partial class GettingStarted
    {
        public static GettingStarted FromJson(string json)
        {
            return JsonConvert.DeserializeObject<GettingStarted>(json, Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this GettingStarted self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}

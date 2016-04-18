using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FitBitPortable.OAuth2
{
    [DataContract()]
    [JsonObject(MemberSerialization.OptIn)]
    public class OAuth2Data
    {
        [DataMember]
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "expires_in")]
        public double ExpiresIn { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }
    }
}

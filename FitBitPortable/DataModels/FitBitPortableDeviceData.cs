using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

using FitBitPortable.Exceptions;

namespace FitBitPortable.DataModels
{
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class DeviceData
    {
        [DataMember]
        [JsonProperty(PropertyName = "battery")]
        public string Battery { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "deviceVersion")]
        public string DeviceVersion { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "lastSyncTime")]
        public string LastSyncTime { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "mac")]
        public string Mac { get; set; }
    }
}

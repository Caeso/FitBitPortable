using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace FitBitPortable.DataModels
{
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class HeartRateZone
    {
        [DataMember]
        [JsonProperty(PropertyName = "caloriesOut")]
        public double CaloriesOut { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "max")]
        public double Max { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "min")]
        public double Min { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "minutes")]
        public double Minutes { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}

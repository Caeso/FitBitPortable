using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace FitBitPortable.DataModels
{
    public enum HeartRateTimeSeriesPeriod { Period1d = 0, Period7d, Period30d, Period1w, Period1m }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class HeartRateTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-heart")]
        public List<HeartRateActivities> ActivitiesHeart { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class HeartRateActivities
    {
        [DataMember]
        [JsonProperty(PropertyName = "dateTime")]
        public string DateTime { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "value")]
        public HeartRateValue Value { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class HeartRateValue
    {
        // ToDo: customHeartRateZones[]

        [DataMember]
        [JsonProperty(PropertyName = "heartRateZones")]
        public List<HeartRateZone> HeartRateZones { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "restingHeartRate")]
        public double RestingHeartRate { get; set; }
    }
}

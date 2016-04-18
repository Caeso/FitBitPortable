using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace FitBitPortable.DataModels
{
    public enum HeartRateIntradayDetailLevel { DetailLevel1sec = 0, DetailLevel1min }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class HeartRateIntradayTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-heart")]
        public List<HeartRateIntradayActivitiesHeart> ActivitiesHeart { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "activities-heart-intraday")]
        public HeartRateIntradayActivitiesHeartIntraday ActivitiesHeartIntraday { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class HeartRateIntradayActivitiesHeart
    {
        // ToDo: customHeartRateZones[]

        [DataMember]
        [JsonProperty(PropertyName = "heartRateZones")]
        public List<HeartRateZone> HeartRateZones { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "dateTime")]
        public string DateTime { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class HeartRateIntradayActivitiesHeartIntraday
    {
        [DataMember]
        [JsonProperty(PropertyName = "dataset")]
        public List<HeartRateIntradayDataset> Dataset { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "datasetInterval")]
        public double DatasetInterval { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "datasetType")]
        public string DatasetType { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class HeartRateIntradayDataset
    {
        [DataMember]
        [JsonProperty(PropertyName = "time")]
        public string Time { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "value")]
        public double Value { get; set; }
    }
}

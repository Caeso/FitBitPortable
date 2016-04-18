using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace FitBitPortable.DataModels
{
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityData
    {
        [DataMember]
        [JsonProperty(PropertyName = "activityId")]
        public string ActivityId { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "activityParentId")]
        public string ActivityParentId { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "activityParentName")]
        public string ActivityParentName { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "calories")]
        public string Calories { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "distance")]
        public string Distance { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "duration")]
        public string Duration { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "hasStartTime")]
        public string HasStartTime { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "isFavorite")]
        public string IsFavorite { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "lastModified")]
        public string LastModified { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "logId")]
        public string LogId { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "startDate")]
        public string StartDate { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "startTime")]
        public string StartTime { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "steps")]
        public string Steps { get; set; }
    }
}

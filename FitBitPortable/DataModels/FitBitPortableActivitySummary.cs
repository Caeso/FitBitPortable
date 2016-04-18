using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace FitBitPortable.DataModels
{
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivitySummary
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities")]
        public List<ActivityData> Activities { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "goals")]
        public Goals Goals { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "summary")]
        public Summary Summary { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class Goals
    {
        [DataMember]
        [JsonProperty(PropertyName = "activeMinutes")]
        public double ActiveMinutes { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "caloriesOut")]
        public double CaloriesOut { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "distance")]
        public double Distance { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "floors")]
        public double Floors { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "steps")]
        public double Steps { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class Summary
    {
        [DataMember]
        [JsonProperty(PropertyName = "activeScore")]
        public double ActiveScore { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "activityCalories")]
        public double ActivityCalories { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "caloriesBMR")]
        public double CaloriesBMR { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "caloriesOut")]
        public double CaloriesOut { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "distances")]
        public List<DistanceData> Distances { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "elevation")]
        public double Elevation { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "fairlyActiveMinutes")]
        public double FairlyActiveMinutes { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "floors")]
        public double Floors { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "lightlyActiveMinutes")]
        public double LightlyActiveMinutes { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "marginalCalories")]
        public double MarginalCalories { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "sedentaryMinutes")]
        public double SedentaryMinutes { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "steps")]
        public double Steps { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "veryActiveMinutes")]
        public double VeryActiveMinutes { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class DistanceData
    {
        [DataMember]
        [JsonProperty(PropertyName = "activity")]
        public string Activity { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "distance")]
        public string Distance { get; set; }
    }
}

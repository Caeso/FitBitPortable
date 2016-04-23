using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FitBitPortable.DataModels
{
    public enum ActivitiyTimeSeriesPeriod { Period1d = 0, Period7d, Period30d, Period1w, Period1m, Period3m, Period6m, Period1y, PeriodMax }
    public enum ActivitiyTimeSeriesResource
    {
        Calories = 0, CaloriesBMR, Steps, Distance, Floors, Elevation, MinutesSedentary, MinutesLightlyActive, MinutesFairlyActive, MinutesVeryActive, ActivityCalories,
        TrackerCalories, TrackerSteps, TrackerDistance, TrackerFloors, TrackerElevation, TrackerMinutesSedentary, TrackerMinutesLightlyActive, TrackerMinutesFairlyActive,
        TrackerMinutesVeryActive, TrackerActivityCalories
    }

    public static class ActivitiyTimeSeriesResourceDictonary
    {
        public static Dictionary<ActivitiyTimeSeriesResource, string> dict = new Dictionary<ActivitiyTimeSeriesResource, string>()
        {
            { ActivitiyTimeSeriesResource.Calories, "calories" },
            { ActivitiyTimeSeriesResource.CaloriesBMR, "caloriesBMR" },
            { ActivitiyTimeSeriesResource.Steps, "steps" },
            { ActivitiyTimeSeriesResource.Distance, "distance" },
            { ActivitiyTimeSeriesResource.Floors, "floors" },
            { ActivitiyTimeSeriesResource.Elevation, "elevation" },
            { ActivitiyTimeSeriesResource.MinutesSedentary, "minutesSedentary" },
            { ActivitiyTimeSeriesResource.MinutesLightlyActive, "minutesLightlyActive" },
            { ActivitiyTimeSeriesResource.MinutesFairlyActive, "minutesFairlyActive" },
            { ActivitiyTimeSeriesResource.MinutesVeryActive, "minutesVeryActive" },
            { ActivitiyTimeSeriesResource.ActivityCalories, "activityCalories" },
            { ActivitiyTimeSeriesResource.TrackerCalories, "tracker/calories" },
            { ActivitiyTimeSeriesResource.TrackerSteps, "tracker/steps" },
            { ActivitiyTimeSeriesResource.TrackerDistance, "tracker/distance" },
            { ActivitiyTimeSeriesResource.TrackerFloors, "tracker/floors" },
            { ActivitiyTimeSeriesResource.TrackerElevation, "tracker/elevation" },
            { ActivitiyTimeSeriesResource.TrackerMinutesSedentary, "tracker/minutesSedentary" },
            { ActivitiyTimeSeriesResource.TrackerMinutesLightlyActive, "tracker/minutesLightlyActive" },
            { ActivitiyTimeSeriesResource.TrackerMinutesFairlyActive, "tracker/minutesFairlyActive" },
            { ActivitiyTimeSeriesResource.TrackerMinutesVeryActive, "tracker/minutesVeryActive" },
            { ActivitiyTimeSeriesResource.TrackerActivityCalories, "tracker/activityCalories" }
        };
    }
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesCalories : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-calories")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesCaloriesBMR : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-caloriesBMR")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesSteps : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-steps")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesDistance : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-distance")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesFloors : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-floors")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesElevation : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-elevation")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesMinutesSedentary : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-minutesSedentary")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesMinutesLightlyActive : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-minutesLightlyActivey")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesMinutesFairlyActive : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-minutesFairlyActive")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesMinutesVeryActive : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-minutesVeryActive")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesActivityCalories : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-activityCalories")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesTrackerCalories : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-tracker-calories")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesTrackerSteps: ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-tracker-steps")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesTrackerDistance : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-tracker-distance")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesTrackerFloors : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-tracker-floors")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesTrackerElevation : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-tracker-elevation")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesTrackerMinutesSedentary : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-tracker-minutesSedentary")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesTrackerMinutesLightlyActive : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-tracker-minutesLightlyActivey")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesTrackerMinutesFairlyActive : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-tracker-minutesFairlyActive")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesTrackerMinutesVeryActive : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-tracker-minutesVeryActive")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeriesTrackerActivityCalories : ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-tracker-activityCalories")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityTimeSeries
    {
        [DataMember]
        [JsonProperty]
        public virtual List<ActivityLogData> ActivitiesLog { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityLogData
    {
        [DataMember]
        [JsonProperty(PropertyName = "dateTime")]
        public string DateTime { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "value")]
        public double Value { get; set; }
    }
}

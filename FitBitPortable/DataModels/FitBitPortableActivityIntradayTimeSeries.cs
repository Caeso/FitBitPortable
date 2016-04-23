using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FitBitPortable.DataModels
{
    public enum ActivityIntradayDetailLevel { DetailLevel1min = 0, DetailLevel15min }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityIntradayTimeSeriesCalories : ActivityIntradayTimeSeries
    {
        [DataMember]
        [JsonProperty(PropertyName = "activities-calories")]
        public override List<ActivityLogData> ActivitiesLog { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "activities-calories-intraday")]
        public override ActivityIntradayActivitiesLogIntraday ActivitiesLogIntraday { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityIntradayTimeSeries
    {
        [DataMember]
        [JsonProperty]
        public virtual List<ActivityLogData> ActivitiesLog { get; set; }
        [DataMember]
        [JsonProperty]
        public virtual ActivityIntradayActivitiesLogIntraday ActivitiesLogIntraday { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityIntradayActivitiesLogIntraday
    {
        [DataMember]
        [JsonProperty(PropertyName = "dataset")]
        public List<HeartRateIntradayDataset> Dataset { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "datasetInterval")]
        public double DatasetInterval { get; set; }
    }
    
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActivityIntradayDataset
    {
        [DataMember]
        [JsonProperty(PropertyName = "time")]
        public string Time { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "value")]
        public double Value { get; set; }
    }
}

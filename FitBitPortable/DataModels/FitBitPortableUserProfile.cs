using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace FitBitPortable.DataModels
{
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class Badges
    {
        [DataMember]
        [JsonProperty(PropertyName = "badgeGradientEndColor")]
        public string BadgeGradientEndColor { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "badgeGradientStartColor")]
        public string BadgeGradientStartColor { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "badgeType")]
        public string BadgeType { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }
        // cheers
        [DataMember]
        [JsonProperty(PropertyName = "dateTime")]
        public string DateTime { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "earnedMessage")]
        public string EarnedMessage { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "encodedId")]
        public string EncodedId { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "image100px")]
        public string Image100px { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "image125px")]
        public string Image125px { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "image300px")]
        public string Image300px { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "image50px")]
        public string Image50px { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "image75px")]
        public string Image75px { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "marketingDescription")]
        public string MarketingDescription { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "mobileDescription")]
        public string MobileDescription { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "shareImage640px")]
        public string ShareImage640px { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "shareText")]
        public string ShareText { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "shortDescription")]
        public string ShortDescription { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "shortName")]
        public string ShortName { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "timesAchieved")]
        public double TimesAchieved { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "value")]
        public double Value { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class SedentaryTime
    {
        [DataMember]
        [JsonProperty(PropertyName = "duration")]
        public double Duration { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "startTime")]
        public string StartTime { get; set; }
    }

    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class UserProfile
    {
        [DataMember]
        [JsonProperty(PropertyName = "age")]
        public double Age { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "avatar")]
        public string Avatar { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "avatar150")]
        public string Avatar150 { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "averageDailySteps")]
        public double AverageDailySteps { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "corporate")]
        public bool Corporate { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "dateOfBirth")]
        public string DateOfBirth { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "distanceUnit")]
        public string DistanceUnit { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "encodedId")]
        public string EncodedId { get; set; }

        // features

        [DataMember]
        [JsonProperty(PropertyName = "foodsLocale")]
        public string FoodsLocale { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "glucoseUnit")]
        public string GlucoseUnit { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "height")]
        public double Height { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "heightUnit")]
        public string HeightUnit { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "memberSince")]
        public string MemberSince { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "offsetFromUTCMillis")]
        public double OffsetFromUTCMillis { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "sedentaryTime")]
        public SedentaryTime SedentaryTime { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "startDayOfWeek")]
        public string StartDayOfWeek { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "strideLengthRunning")]
        public double StrideLengthRunning { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "strideLengthRunningType")]
        public string StrideLengthRunningType { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "strideLengthWalking")]
        public double StrideLengthWalking { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "strideLengthWalkingType")]
        public string StrideLengthWalkingType { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "timezone")]
        public string Timezone { get; set; }
        [DataMember]
        [JsonProperty(PropertyName = "topBadges")]
        public List<Badges> TopBadges { get; set; }
    }
}

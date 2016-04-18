using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using FitBitPortable.OAuth2;
using FitBitPortable.Exceptions;

namespace FitBitPortable.DataModels
{
    public static class JsonConverter
    {
        public static OAuth2Data OAuth2Data(string JsonData)
        {
            OAuth2Data OAuth2;

            try
            {
                OAuth2 = JsonConvert.DeserializeObject<OAuth2Data>(JsonData);
            }
            catch (Exception e)
            {
                throw new FitBitApiOAuth2Exception();
            };

            return OAuth2;
        }

        internal static List<DeviceData> DeviceData(string JsonData)
        {
            List<DeviceData> DeviceDataList = new List<DeviceData>();

            try
            {
                DeviceDataList = JsonConvert.DeserializeObject<List<DeviceData>>(JsonData);
            }
            catch (Exception e)
            {
                throw new FitBitApiDeviceDataException();
            };

            return DeviceDataList;
        }

        internal static UserProfile UserProfile(string JsonData)
        {
            UserProfile user;
            try
            {
                JToken userdata = JObject.Parse(JsonData)["user"];
                user = JsonConvert.DeserializeObject<UserProfile>(userdata.ToString());
            }
            catch (Exception e)
            {
                throw new FitBitApiUserProfileException();
            };

            return user;
        }

        internal static ActivitySummary ActivitySummary(string JsonData)
        {
            ActivitySummary summary;
            try
            {
                summary = JsonConvert.DeserializeObject<ActivitySummary>(JsonData);
            }
            catch (Exception e)
            {
                throw new FitBitApiActivityDailySummaryException();
            };

            return summary;
        }

        internal static HeartRateIntradayTimeSeries HeartRateIntradayTimeSeries(string JsonData)
        {
            HeartRateIntradayTimeSeries heartratedata;
            try
            {
                heartratedata = JsonConvert.DeserializeObject<HeartRateIntradayTimeSeries>(JsonData);
            }
            catch (Exception e)
            {
                throw new FitBitApiHeartRateIntradayTimeSeriesException();
            };

            return heartratedata;
        }

        internal static HeartRateTimeSeries HeartRateTimeSeries(string JsonData)
        {
            HeartRateTimeSeries heartratedata;
            try
            {
                heartratedata = JsonConvert.DeserializeObject<HeartRateTimeSeries>(JsonData);
            }
            catch (Exception e)
            {
                throw new FitBitApiHeartRateTimeSeriesException();
            };

            return heartratedata;
        }
    }
}

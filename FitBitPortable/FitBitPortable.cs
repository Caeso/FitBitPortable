using System;
using System.Collections.Generic;

using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using FitBitPortable.DataModels;
using FitBitPortable.OAuth2;
using FitBitPortable.Exceptions;

namespace FitBitPortable
{
    public interface IFitBitPortableClient
    {
        Task<bool> Authentication(string ClientId, string ClientSecret, string AuthenticationRequestUri, string TokenRequestUri, string RedirectUri);
        Task<bool> RefreshToken(string ClientId, string ClientSecret, string TokenRequestUri);

        Task<List<DeviceData>> GetDeviceData();
    }

    abstract public class FitBitPortableClient : IFitBitPortableClient
    {
        protected HttpClient HttpClient { get; set; }

        protected Secrets Secrets { get; set;  }
        protected OAuth2Data OAuth2Data { get; set; }

        abstract public Task<bool> Authentication(string ClientId, string ClientSecret, string AuthenticationRequestUri, string TokenRequestUri, string RedirectUri);

        public async Task<bool> RefreshToken(string ClientId, string ClientSecret, string TokenRequestUri)
        {
            var HeaderToken = System.Text.Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret);
            var HeaderTokenBase64 = System.Convert.ToBase64String(HeaderToken);
            var Header = new AuthenticationHeaderValue("Basic", HeaderTokenBase64);

            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Authorization = Header;

            List<KeyValuePair<string, string>> RefreshTokenRequestData = new List<KeyValuePair<string, string>>();

            RefreshTokenRequestData.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            RefreshTokenRequestData.Add(new KeyValuePair<string, string>("refresh_token", OAuth2Data.RefreshToken));

            HttpContent RefreshTokenRequestDataContent = new FormUrlEncodedContent(RefreshTokenRequestData);

            HttpResponseMessage RefreshTokenRequestResponse = await HttpClient.PostAsync(new Uri(TokenRequestUri), RefreshTokenRequestDataContent);

            switch (RefreshTokenRequestResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                default:
                    return false;
            }

            string JsonData = await RefreshTokenRequestResponse.Content.ReadAsStringAsync();
            OAuth2Data = DataModels.JsonConverter.OAuth2Data(JsonData); 

            return true;
        }

        public async Task<List<DeviceData>> GetDeviceData()
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(OAuth2Data.TokenType, OAuth2Data.AccessToken);

            // GET https://api.fitbit.com/1/user/-/devices.json
            Uri RequestUri = new Uri("https://api.fitbit.com/1/user/-/devices.json");

            HttpResponseMessage Response = await HttpClient.GetAsync(RequestUri);
            string JsonData = await Response.Content.ReadAsStringAsync();

            return DataModels.JsonConverter.Generic<List<DeviceData>>(JsonData);
        }

        public async Task<UserProfile> GetUserProfile()
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(OAuth2Data.TokenType, OAuth2Data.AccessToken);

            // GET https://api.fitbit.com/1/user/[user-id]/profile.json
            // user-id The encoded ID of the user. Use "-" (dash) for current logged-in user
            Uri RequestUri = new Uri("https://api.fitbit.com/1/user/-/profile.json");

            HttpResponseMessage Response = await HttpClient.GetAsync(RequestUri);
            string JsonData = await Response.Content.ReadAsStringAsync();

            return DataModels.JsonConverter.UserProfile(JsonData);
        }

        public async Task<ActivitySummary> GetActivitySummary(DateTimeOffset Date)
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(OAuth2Data.TokenType, OAuth2Data.AccessToken);

            // GET https://api.fitbitcom/1/user/[user-id]/activities/date/[date].json
            // user-id The encoded ID of the user. Use "-" (dash) for current logged-in user
            // date The date in the format yyyy-MM-dd 
            Uri RequestUri = new Uri("https://api.fitbit.com/1/user/-/activities/date/" + Date.ToString("yyyy-MM-dd") + ".json");

            HttpResponseMessage Response = await HttpClient.GetAsync(RequestUri);
            string JsonData = await Response.Content.ReadAsStringAsync();

            return DataModels.JsonConverter.Generic<ActivitySummary>(JsonData);
        }

        public async Task<ActivityTimeSeries> GetActivityTimeSeries(ActivitiyTimeSeriesResource Resource, DateTimeOffset BaseDate, DateTimeOffset EndDate)
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(OAuth2Data.TokenType, OAuth2Data.AccessToken);

            // GET /1/user/[user-id]/[resource-path]/date/[date]/[period].json
            // GET /1/user/[user-id]/[resource-path]/date/[base-date]/[end-date].json
            // user-id The encoded ID of the user. Use "-" (dash) for current logged-in user. 
            // resource-path The resource path; see options in the "Resource Path Options" section below. 
            // base-date The range start date, in the format yyyy-MM-dd or today. 
            // end-date The end date of the range. 
            // date The end date of the period specified in the format yyyy-MM-dd or today. 
            // period The range for which dict will be returned. Options are 1d, 7d, 30d, 1w, 1m, 3m, 6m, 1y, or max.

            string resource = ActivitiyTimeSeriesResourceDictonary.dict[Resource];

            Uri RequestUri = new Uri("https://api.fitbit.com/1/user/-/activities/"+
                resource + "/" +
                "date" + "/" +
                BaseDate.ToString("yyyy-MM-dd") + "/" +
                EndDate.ToString("yyyy-MM-dd") + ".json");

            HttpResponseMessage Response = await HttpClient.GetAsync(RequestUri);
            string JsonData = await Response.Content.ReadAsStringAsync();

            switch (Resource)
            {
                case ActivitiyTimeSeriesResource.Calories:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesCalories>(JsonData);
                case ActivitiyTimeSeriesResource.CaloriesBMR:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesCaloriesBMR>(JsonData);
                case ActivitiyTimeSeriesResource.Steps:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesSteps>(JsonData);
                case ActivitiyTimeSeriesResource.Distance:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesDistance>(JsonData);
                case ActivitiyTimeSeriesResource.Floors:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesFloors>(JsonData);
                case ActivitiyTimeSeriesResource.Elevation:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesElevation>(JsonData);
                case ActivitiyTimeSeriesResource.MinutesSedentary:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesMinutesSedentary>(JsonData);
                case ActivitiyTimeSeriesResource.MinutesLightlyActive:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesMinutesLightlyActive>(JsonData);
                case ActivitiyTimeSeriesResource.MinutesVeryActive:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesMinutesVeryActive>(JsonData);
                case ActivitiyTimeSeriesResource.ActivityCalories:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesActivityCalories>(JsonData);
                case ActivitiyTimeSeriesResource.TrackerCalories:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesTrackerCalories>(JsonData);
                case ActivitiyTimeSeriesResource.TrackerSteps:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesTrackerSteps>(JsonData);
                case ActivitiyTimeSeriesResource.TrackerDistance:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesTrackerDistance>(JsonData);
                case ActivitiyTimeSeriesResource.TrackerFloors:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesTrackerFloors>(JsonData);
                case ActivitiyTimeSeriesResource.TrackerElevation:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesTrackerElevation>(JsonData);
                case ActivitiyTimeSeriesResource.TrackerMinutesSedentary:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesTrackerMinutesSedentary>(JsonData);
                case ActivitiyTimeSeriesResource.TrackerMinutesLightlyActive:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesTrackerMinutesLightlyActive>(JsonData);
                case ActivitiyTimeSeriesResource.TrackerMinutesVeryActive:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesTrackerMinutesVeryActive>(JsonData);
                case ActivitiyTimeSeriesResource.TrackerActivityCalories:
                    return DataModels.JsonConverter.Generic<ActivityTimeSeriesTrackerActivityCalories>(JsonData);
            }

            return null;
        }

        public async Task<HeartRateTimeSeries> GetHeartRateTimeSeries(DateTimeOffset BaseDate, DateTimeOffset EndDate)
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(OAuth2Data.TokenType, OAuth2Data.AccessToken);

            // GET https://api.fitbit.com/1/user/[user-id]/activities/heart/date/[date]/[period].json
            // GET https://api.fitbit.com/1/user/[user-id]/activities/heart/date/[base-date]/[end-date].json
            // user-id The encoded ID of the user. Use "-" (dash) for current logged-in user. 
            // base-date The range start date, in the format yyyy-MM-dd or today. 
            // end-date The end date of the range. 
            // date The end date of the period specified in the format yyyy-MM-dd or today. 
            // period The range for which dict will be returned. Options are 1d, 7d, 30d, 1w, 1m. 

            Uri RequestUri = new Uri(
                "https://api.fitbit.com/1/user/-/activities/heart/date/" +
                BaseDate.ToString("yyyy-MM-dd") + "/" +
                EndDate.ToString("yyyy-MM-dd") + ".json"
                );

            HttpResponseMessage HeartRateDataResponse = await HttpClient.GetAsync(RequestUri);
            string JsonData = await HeartRateDataResponse.Content.ReadAsStringAsync();

            return DataModels.JsonConverter.Generic<HeartRateTimeSeries>(JsonData);
        }

        public async Task<List<HeartRateIntradayTimeSeries>> GetHeartRateIntradayTimeSeries(DateTimeOffset Start, DateTimeOffset End, HeartRateIntradayDetailLevel DetailLevel)
        {
            List<HeartRateIntradayTimeSeries> HeartRateDataList = new List<HeartRateIntradayTimeSeries>();

            TimeSpan interval = new TimeSpan(22, 00, 0);
            TimeSpan period = End - Start;

            int timeintervals = (int)(period.Ticks / interval.Ticks);
            TimeSpan rest = new TimeSpan(period.Ticks % interval.Ticks);

            DateTimeOffset act = Start;
            for (int i = 0; i < timeintervals; i++)
            {
                HeartRateDataList.Add(await GetHeartRateIntradayTimeSeries(act, interval, DetailLevel));
                act += interval;
            }
            HeartRateDataList.Add(await GetHeartRateIntradayTimeSeries(act, rest, DetailLevel));

            return HeartRateDataList;
        }

        private async Task<HeartRateIntradayTimeSeries> GetHeartRateIntradayTimeSeries(DateTimeOffset Start, TimeSpan Interval, HeartRateIntradayDetailLevel DetailLevel)
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(OAuth2Data.TokenType, OAuth2Data.AccessToken);


            // GET https://api.fitbit.com/1/user/-/activities/heart/date/[date]/[end-date]/[detail-level]/time/[start-time]/[end-time].json
            // date The date, in the format yyyy-MM-dd or today 
            // detail-level Number of dict points to include. Either 1sec or 1min. Optional.
            // start-time The start of the period, in the format HH:mm. Optional. 
            // end-time The end of the period, in the format HH:mm. Optional.
            Uri RequestUri = new Uri(
                "https://api.fitbit.com/1/user/-/activities/heart/date/" +
                Start.ToString("yyyy-MM-dd") + "/" +
                (Start + Interval).ToString("yyyy-MM-dd") + "/" +
                (DetailLevel == HeartRateIntradayDetailLevel.DetailLevel1sec ? "1sec" : "1min") + "/" +
                "time" + "/" +
                Start.ToString("HH:mm") + "/" +
                (Start + Interval).ToString("HH:mm") + ".json"
                );

            HttpResponseMessage HeartRateDataResponse = await HttpClient.GetAsync(RequestUri);
            string JsonData = await HeartRateDataResponse.Content.ReadAsStringAsync();

            return DataModels.JsonConverter.Generic<HeartRateIntradayTimeSeries>(JsonData);
        }
    }

}

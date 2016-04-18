using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

using FitBitPortable;
using FitBitPortable.DataModels;
using FitBitPortable.Exceptions;
using FitBitPortable.OAuth2;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ConsoleApplication
{
    class Program
    {
        static MySecrets _Secrets = new MySecrets();

        const String _AuthRequestUrl = "https://www.fitbit.com/oauth2/authorize";
        const String _TokenRequestUrl = "https://api.fitbit.com/oauth2/token";
        const String _RedirectUrl = "https://fam-nass.de";

        static void Main(string[] args)
        {
            FitBitConsoleClient client = new FitBitConsoleClient();
            var x = client.Authentication(_Secrets.ClientId, _Secrets.ClientSecret, _AuthRequestUrl, _TokenRequestUrl, _RedirectUrl);
            x.Wait();

            var user = client.GetUserProfile();
            user.Wait();
            var devices = client.GetDeviceData();
            devices.Wait();
            var heartrateseries = client.GetHeartRateIntradayTimeSeries(new DateTime(2016, 4, 17, 0, 0, 0), new DateTime(2016, 4, 17, 12, 0, 0), HeartRateIntradayDetailLevel.DetailLevel1sec);
            heartrateseries.Wait();

            var user_data = user.Result;
            var device_data = devices.Result;
            var heartratedata = heartrateseries.Result;
        }
    }

    class FitBitConsoleClient : FitBitPortableClient
    {
        public FitBitConsoleClient()
        {
            this.HttpClient = new HttpClient();
            this.OAuth2Data = new OAuth2Data();
        }

        public async override Task<bool> Authentication(string ClientId, string ClientSecret, string AuthenticationRequestUri, string TokenRequestUri, string RedirectUri)
        {
            var AuthRequestUri = new Uri(AuthenticationRequestUri +
                 "?client_id=" + ClientId +
                 "&response_type=code" +
                 "&scope=activity heartrate location nutrition profile settings sleep social weight" +
                 "&redirect_uri=" + Uri.EscapeUriString(RedirectUri) +
                 "&expires_in=2592000" +
                 "&prompt=none");
            var AuthCallbackUri = new Uri(RedirectUri);

            var uri = AuthRequestUri.AbsoluteUri;

            IWebDriver driver = new FirefoxDriver();

            driver.Navigate().GoToUrl(AuthRequestUri.AbsoluteUri);

            // https://fam-nass.de/?code=18cad1470abaafee8c30994d1f0dfe791ba727a7#success
            string code;
            do
            {
                Console.WriteLine("URL: {0}", driver.Url);
                Uri url = new Uri(driver.Url);
                NameValueCollection x = HttpUtility.ParseQueryString(url.Query, Encoding.ASCII);
                code = x.Get("code");
                System.Threading.Thread.Sleep(1000);

            } while (code == null);

            var HeaderToken = System.Text.Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret);
            var HeaderTokenBase64 = System.Convert.ToBase64String(HeaderToken);
            var Header = new AuthenticationHeaderValue("Basic", HeaderTokenBase64);

            this.HttpClient.DefaultRequestHeaders.Clear();
            this.HttpClient.DefaultRequestHeaders.Authorization = Header;

            List<KeyValuePair<string, string>> TokenRequestData = new List<KeyValuePair<string, string>>();

            TokenRequestData.Add(new KeyValuePair<string, string>("code", code));
            TokenRequestData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            TokenRequestData.Add(new KeyValuePair<string, string>("client_id", ClientId));
            TokenRequestData.Add(new KeyValuePair<string, string>("redirect_uri", Uri.EscapeUriString(RedirectUri)));

            HttpContent TokenRequestDataContent = new FormUrlEncodedContent(TokenRequestData);

            HttpResponseMessage TokenRequestResponse = await this.HttpClient.PostAsync(new Uri(TokenRequestUri), TokenRequestDataContent);

            switch (TokenRequestResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                default:
                    return false;
            }

            string JsonData = await TokenRequestResponse.Content.ReadAsStringAsync();
            this.OAuth2Data = JsonConverter.OAuth2Data(JsonData);

            return true;
        }
    }
}
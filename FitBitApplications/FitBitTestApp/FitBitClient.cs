using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

using Windows.Security.Authentication.Web;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Windows.Storage;


using FitBitPortable;
using FitBitPortable.DataModels;
using FitBitPortable.Exceptions;
using FitBitPortable.OAuth2;

namespace FitBit
{
    public class FitBitClient : FitBitPortableClient
    {
        const String _AuthRequestUrl = "https://www.fitbit.com/oauth2/authorize";
        const String _TokenRequestUrl = "https://api.fitbit.com/oauth2/token";
        const String _RedirectUrl = "https://fam-nass.de";

        private static MySecrets _MySecrets = new MySecrets();

        public FitBitClient()
        {
            this.HttpClient = new System.Net.Http.HttpClient();
            this.OAuth2Data = new OAuth2Data();
        }

        public async Task<bool> Authentication()
        {
            return await Authentication(_MySecrets.ClientId, _MySecrets.ClientSecret, _AuthRequestUrl, _TokenRequestUrl, _RedirectUrl);
        }

        public override async Task<bool> Authentication(string ClientId, string ClientSecret, string AuthenticationRequestUri, string TokenRequestUri, string RedirectUri)
        {
            HttpClient _Client = new HttpClient();

            var AuthRequestUri = new Uri(AuthenticationRequestUri +
                "?client_id=" + ClientId +
                "&response_type=code" +
                "&scope=activity heartrate location nutrition profile settings sleep social weight" +
                "&redirect_uri=" + Uri.EscapeUriString(RedirectUri) +
                "&expires_in=2592000" +
                "&prompt=silent");
            var AuthCallbackUri = new Uri(RedirectUri);

            WebAuthenticationResult AuthRequestResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, AuthRequestUri, AuthCallbackUri);

            switch (AuthRequestResult.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    break;
                default:
                    return false;
            }

            string _Code = DataExchangeClientExtensions.ParseResultData(AuthRequestResult.ResponseData);

            var HeaderToken = System.Text.Encoding.UTF8.GetBytes(ClientId + ":" + ClientSecret);
            var HeaderTokenBase64 = System.Convert.ToBase64String(HeaderToken);
            var Header = new HttpCredentialsHeaderValue("Basic", HeaderTokenBase64);

            _Client.DefaultRequestHeaders.Clear();
            _Client.DefaultRequestHeaders.Authorization = Header;

            List<KeyValuePair<string, string>> TokenRequestData = new List<KeyValuePair<string, string>>();

            TokenRequestData.Add(new KeyValuePair<string, string>("code", _Code));
            TokenRequestData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            TokenRequestData.Add(new KeyValuePair<string, string>("client_id", ClientId));
            TokenRequestData.Add(new KeyValuePair<string, string>("redirect_uri", Uri.EscapeUriString(_RedirectUrl)));

            IHttpContent TokenRequestDataContent = new HttpFormUrlEncodedContent(TokenRequestData);

            HttpResponseMessage TokenRequestResponse = await _Client.PostAsync(new Uri(TokenRequestUri), TokenRequestDataContent);

            switch (TokenRequestResponse.StatusCode)
            {
                case HttpStatusCode.Ok:
                    break;
                default:
                    return false;
            }

            string JsonData = await TokenRequestResponse.Content.ReadAsStringAsync();

            this.OAuth2Data = JsonConverter.OAuth2Data(JsonData);

            return true;
        }


        public async Task<bool> RefreshToken()
        {
            return await RefreshToken(_MySecrets.ClientId, _MySecrets.ClientSecret, _TokenRequestUrl);
        }

        ApplicationDataContainer _DataContainer;
        StorageFolder _StorageFolder;

        public void Save()
        {
            _DataContainer = ApplicationData.Current.LocalSettings;
            _StorageFolder = ApplicationData.Current.LocalFolder;

            ApplicationDataCompositeValue AuthenticationData = new ApplicationDataCompositeValue();
            AuthenticationData["AccessToken"] = this.OAuth2Data.AccessToken;
            AuthenticationData["ExpiresIn"] = this.OAuth2Data.ExpiresIn;
            AuthenticationData["RefreshToken"] = this.OAuth2Data.RefreshToken;
            AuthenticationData["Scope"] = this.OAuth2Data.Scope;
            AuthenticationData["TokenType"] = this.OAuth2Data.TokenType;
            AuthenticationData["UserId"] = this.OAuth2Data.UserId;

            _DataContainer.Values["AuthenticationData"] = AuthenticationData;
        }

        public Boolean Restore()
        {
            _DataContainer = ApplicationData.Current.LocalSettings;
            _StorageFolder = ApplicationData.Current.LocalFolder;

            ApplicationDataCompositeValue AuthenticationData = (ApplicationDataCompositeValue)_DataContainer.Values["AuthenticationData"];
            if (AuthenticationData == null) return false;

            this.OAuth2Data.AccessToken = (string)AuthenticationData["AccessToken"];
            this.OAuth2Data.ExpiresIn = (double)AuthenticationData["ExpiresIn"];
            this.OAuth2Data.RefreshToken = (string)AuthenticationData["RefreshToken"];
            this.OAuth2Data.Scope = (string)AuthenticationData["Scope"];
            this.OAuth2Data.TokenType = (string)AuthenticationData["TokenType"];
            this.OAuth2Data.UserId = (string)AuthenticationData["UserId"];

            return true;
        }
    }

    public static class DataExchangeClientExtensions
    {
        private static readonly Regex _regex1 = new Regex(@"=(.+#)");
        private static readonly Regex _regex2 = new Regex(@"[?|&]([\w\.]+)=([^?|^&]+)");

        public static string ParseResultData(string data)
        {
            Match match = _regex1.Match(data);

            if (match.Success)
            {
                string temp = match.Value;
                return temp.Substring(1, temp.Length - 2);
            }

            return string.Empty;
        }

        public static IReadOnlyDictionary<string, string> ParseQueryString(this String data)
        {
            var match = _regex2.Match(data);
            var paramaters = new Dictionary<string, string>();
            while (match.Success)
            {
                paramaters.Add(match.Groups[1].Value, match.Groups[2].Value);
                match = match.NextMatch();
            }
            return paramaters;
        }

        public static IReadOnlyDictionary<string, string> ParseQueryString(this Uri uri)
        {
            var match = _regex2.Match(uri.PathAndQuery);
            var paramaters = new Dictionary<string, string>();
            while (match.Success)
            {
                paramaters.Add(match.Groups[1].Value, match.Groups[2].Value);
                match = match.NextMatch();
            }
            return paramaters;
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace TrackMyWalks.Services
{
    public class TwitterWebService : ITwitterWebService
    {
        // Declare our HttpClient Manager objects
        HttpClient client;

        public TwitterWebService()
        {
            this.client = new HttpClient();
            client.BaseAddress = new Uri("https://api.twitter.com/1.1");
            client.MaxResponseContentBufferSize = 256000;
        }

        // Gets the users Twitter Profile Information using the supplied
        // Account information
        public async Task<JObject> GetTwitterProfile(Account account)
        {
            // Construct our RequestUrl using our BaseAddress and the Twitter API
            var RequestUrl = new Uri(String.Format($"{client.BaseAddress}/account/verify_credentials.json"));
            // Get our profile information using the RequestUrl and the account
            // information of the user
            var oRequest = new OAuth1Request("GET", RequestUrl, null, account);
            var response = await oRequest.GetResponseAsync();
            // Return the response object back to the caller
            return JObject.Parse(response?.GetResponseText());
        }

        // Sends a twitter message using the supplied Account information
        public async Task<string> TweetMessage(string message, Account account)
        {
            // Construct our RequestUrl using our BaseAddress and the Twitter API
            var RequestUrl = new Uri(String.Format($"{client.BaseAddress}/statuses/update.json"));
            // Add the Authentication headers that are required for the request
            var oAuthData = new Dictionary<string, string>();
            oAuthData.Add("status", message);
            oAuthData.Add("trim_user", "1");
            // Post the Tweet, using the RequestUrl and oAuthData header information
            var oRequest = new OAuth1Request("POST", RequestUrl, oAuthData, account);
            var response = await oRequest.GetResponseAsync();
            // Return the response string back to the caller
            return response?.GetResponseText();
        }
    }
}

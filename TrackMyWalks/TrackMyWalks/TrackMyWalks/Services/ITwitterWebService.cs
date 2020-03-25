using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace TrackMyWalks.Services
{
    public interface ITwitterWebService
    {
        // Instance method to get the user's Twitter Profile Details
        Task<JObject> GetTwitterProfile(Account e);
        // Instance method to post a Tweet message to the users Twitter Feed
        Task<string> TweetMessage(string message, Account e);
    }
}

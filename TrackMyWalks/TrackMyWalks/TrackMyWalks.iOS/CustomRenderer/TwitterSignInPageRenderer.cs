using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TrackMyWalks.iOS.CustomRenderer;
using TrackMyWalks.Services;
using TrackMyWalks.Views;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TwitterSignInPage), typeof(TwitterSignInPageRenderer))]
namespace TrackMyWalks.iOS.CustomRenderer
{
    public class TwitterSignInPageRenderer : PageRenderer
    {
        string oAuth_Token = String.Empty;
        string oAuth_Token_Secret = String.Empty;
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            // Instance method that will display a Twitter Sign In Page
            var auth = new OAuth1Authenticator(
            consumerKey: TwitterAuthDetails.ConsumerKey,
            consumerSecret: TwitterAuthDetails.ConsumerSecret,
            requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
            authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
            accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
            callbackUrl: new Uri("https://mobile.twitter.com/home"));
            // Prevent displaying the Cancel button on the Twitter sign on page
            auth.AllowCancel = false;
            // Define our completion handler once the user has successfully signed in
            auth.Completed += (object sender, AuthenticatorCompletedEventArgs e) =>
            {
                if (e.IsAuthenticated)
                {
                    e.Account.Properties.TryGetValue("oauth_token", out oAuth_Token);
                    e.Account.Properties.TryGetValue("oauth_token_secret", out oAuth_Token_Secret);
                    // Instantiate our class to Store our Twitter Authentication Token
                    TwitterAuthDetails.StoreAuthToken(oAuth_Token);
                    TwitterAuthDetails.StoreTokenSecret(oAuth_Token_Secret);
                    TwitterAuthDetails.StoreAccountDetails(e.Account);
                }
                // Dismiss our Twitter Authentication UI Dialog
                DismissViewController(true, () =>
                {
                });
            };
            PresentViewController(auth.GetUI(), true, null);
        }
    }
}
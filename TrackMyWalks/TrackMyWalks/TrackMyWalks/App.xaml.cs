using System;
using TrackMyWalks.Models;
using TrackMyWalks.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackMyWalks
{
    public partial class App : Application
    {
        public static WalkDataModel SelectedItem { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Check what Target OS Platform we are running on whenever the app starts
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                MainPage = new SplashPage();
            }
            else
            {
                // Set the root page for our application
                MainPage = new NavigationPage(new WalksMainPage());
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

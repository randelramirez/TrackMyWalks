﻿using System;
using System.Threading.Tasks;
using TrackMyWalks.Models;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using TrackMyWalks.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackMyWalks
{
    public partial class App : Application
    {
        public static WalkDataModel SelectedItem { get; set; }
        public static NavigationService NavService { get; set; }

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            // Initialise and create an instance of our navigation
            // service class
            NavService = DependencyService.Get<INavigationService>() as NavigationService;
        }

        protected override void OnStart()
        {
            // Check what Target OS Platform we are running on whenever the app starts
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                // Set the Root Page for our Application
                MainPage = new NavigationPage(new SplashPage());
            }
            else
            {
                // Set the Root Page and update the NavigationBar color for our app
                MainPage = new NavigationPage(new WalksMainPage())
                {
                    BarBackgroundColor = Color.IndianRed,
                    BarTextColor = Color.White,
                };
            }

            // Set the current main page to our Navigation Service
            NavService.XFNavigation = MainPage.Navigation;
            // Register each of our View Models on our Navigation Stack
            NavService.RegisterViewMapping(typeof(WalksMainPageViewModel), typeof(WalksMainPage));
            NavService.RegisterViewMapping(typeof(WalkEntryPageViewModel), typeof(WalkEntryPage));
            NavService.RegisterViewMapping(typeof(WalkTrailInfoPageViewModel), typeof(WalkTrailInfoPage));
            NavService.RegisterViewMapping(typeof(WalkDistancePageViewModel), typeof(WalkDistancePage));
            NavService.RegisterViewMapping(typeof(TwitterSignInPageViewModel), typeof(TwitterSignInPage));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #region Twitter Sign In Page Property and Instance methods to remove and Navigate(Android Only)
        // Action property method to remove our TwitterSignInPage from
        // the NavigationStack
        public static Action RemoveTwitterSignInPage =>
            new Action(() => NavService.XFNavigation.PopAsync());

        // Navigate to our WalksMainPage, once we have successfully signed in
        public async static Task NavigateToWalksMainPage()
        {
            await NavService.XFNavigation.PushAsync(new WalksMainPage());
        }
        #endregion
    }
}

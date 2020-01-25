﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackMyWalks.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Set a wait delay of 3 seconds on our Splash Screen
            await Task.Delay(3000);
            // Update the Main Page and update the NavigationBar
            // color for our app
            Application.Current.MainPage = new NavigationPage(new WalksMainPage())
            {
                BarBackgroundColor = Color.CadetBlue,
                BarTextColor = Color.White
            };

            App.NavService.XFNavigation = Application.Current.MainPage.Navigation;
        }
    }
}
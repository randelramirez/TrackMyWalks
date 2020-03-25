using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackMyWalks.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TwitterSignInPage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        TwitterSignInPageViewModel _viewModel => BindingContext as TwitterSignInPageViewModel;

        public TwitterSignInPage()
        {
            // Update the Title and Initialise our BindingContext for the Page
            this.Title = "Track My Walks Twitter Sign In";
            this.BindingContext = new TwitterSignInPageViewModel(DependencyService.Get<INavigationService>());
        }

        // Method to initialise our View Model when the ContentPage appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Check to see if we have logged in and remove our Twitter
            // Sign In Page
            if (_viewModel != null && TwitterAuthDetails.isLoggedIn)
            {
                // Pops our Twitter Sign In Page from our Navigation Stack
                await Navigation.PopAsync();
            }
        }
    }
}
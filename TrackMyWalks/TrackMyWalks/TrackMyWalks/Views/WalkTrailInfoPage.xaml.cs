using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
    public partial class WalkTrailInfoPage : ContentPage
    {
        WalkTrailInfoPageViewModel _viewModel => BindingContext as WalkTrailInfoPageViewModel;

        public WalkTrailInfoPage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            //this.Title = "Trail Walk Information";
            //this.BindingContext = new WalkTrailInfoPageViewModel(DependencyService.Get<INavigationService>());


            // Update the page title for our Walk Information Page
            Title = "Trail Walk Information";
            // Set the Binding Context for our ContentPage
            this.BindingContext = new WalkTrailInfoPageViewModel(DependencyService.Get<INavigationService>());

        }

        // Instance method that proceeds to begin a new walk trail
        public async void BeginTrailWalk_Clicked(object sender, EventArgs e)
        {
            //if (App.SelectedItem == null)
            //    return;

            //await _viewModel.Navigation.NavigateTo<WalkDistancePageViewModel>();

            if (App.SelectedItem == null)
                return;
            await _viewModel.Navigation.NavigateTo<WalkDistancePageViewModel>();
            //Navigation.RemovePage(this);
        }
    }
}
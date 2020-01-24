using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TrackMyWalks.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalkDistancePage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        WalkDistancePageViewModel _viewModel => BindingContext as WalkDistancePageViewModel;

        public WalkDistancePage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            Title = "Distance Travelled Information";
            this.BindingContext = new WalkDistancePageViewModel();


            // Create a pin placeholder within the map containing the
            // walk information
            customMap.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Position = new Position(_viewModel.Latitude, _viewModel.Longitude),
                Label = _viewModel.Title,
                Address = "Difficulty: " + _viewModel.Difficulty + " Total Distance: " + _viewModel.Distance,
                MarkerId = _viewModel.Title
            });
            // Create a region around the map within a one-kilometer radius
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new
            Position(_viewModel.Latitude, _viewModel.Longitude),
            Distance.FromKilometers(1.0)));

        }

        // Instance method that ends the current trail and returns back to the main screen.
        public void EndThisTrailButton_Clicked(object sender, EventArgs e)
        {
            App.SelectedItem = null;
            Navigation.PopToRootAsync(true);
        }
    }
}
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using TrackMyWalks.Views.MapOverlay;
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

        Task<Plugin.Geolocator.Abstractions.Position> origPosition;

        public WalkDistancePage()
        {
            InitializeComponent();

            // Update the Title and Initialise our BindingContext for the Page
            Title = "Distance Travelled Information";
            this.BindingContext = new WalkDistancePageViewModel(DependencyService.Get<INavigationService>());

            #region using defaultmap/oldmap implementation

            // Create a pin placeholder within the map containing the
            // walk information
            //customMap.Pins.Add(new Pin
            //{
            //    Type = PinType.Place,
            //    Position = new Position(_viewModel.Latitude, _viewModel.Longitude),
            //    Label = _viewModel.Title,
            //    Address = "Difficulty: " + _viewModel.Difficulty + " Total Distance: " + _viewModel.Distance,
            //    MarkerId = _viewModel.Title
            //});
            // Create a region around the map within a one-kilometer radius
            //customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new
            //Position(_viewModel.Latitude, _viewModel.Longitude),
            //Distance.FromKilometers(1.0)));

            #endregion


            // Get the current GPS location coordinates and listen
            // for updates
            origPosition = _viewModel.GetCurrentLocation();
            _viewModel.CoordsChanged += Location_CoordsChanged;
            _viewModel.OnStartUpdate();

            // Instantiate our Custom Map Overlay
            customMap = new CustomMapOverlay
            {
                MapType = MapType.Street
            };

            // Clear all previously created Pins on our CustomMap
            customMap.Pins.Clear();

            // Create the Pin placeholder that will represent our
            // current location
            CreatePinPlaceholder(PinType.Place, origPosition.Result.Latitude, origPosition.Result.Longitude, "", "My Location", 1);

            // Create the Pin placeholder that will represent our
            // ending location
            CreatePinPlaceholder(PinType.Place,
            _viewModel.Latitude,
            _viewModel.Longitude,
            _viewModel.Title,
            "Difficulty: " + _viewModel.Difficulty +
            " Total Distance: " + _viewModel.Distance, 2);
            // Add the Starting and Ending Latitude and Longitude
            // Coordinates
            customMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(
            origPosition.Result.Latitude,
            origPosition.Result.Longitude));
            customMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(
            _viewModel.Latitude,
            _viewModel.Longitude));
            // Create and Initialise a map region within a
            // one-kilometre radius
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(
            new Xamarin.Forms.Maps.Position(
            origPosition.Result.Latitude,
            origPosition.Result.Longitude),
            Distance.FromKilometers(1)));
            // Display our Custom Map for the detected device
            // Platform
            Content = customMap;
        }

        // Instance method to handle updating the UI whenever the
        // location changes
        public void Location_CoordsChanged(object sender, PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                // Calculate the total distance traveled from the
                // origPosition to the Current GPS Coordinate
                var distancetraveled = origPosition.Result.CalculateDistance(
                e.Position,
                GeolocatorUtils.DistanceUnits.Kilometers);
                // Create a new Pin Placeholder, showing the current GPS
                // Coordinate and the distance traveled
                CreatePinPlaceholder(PinType.SavedPin,
                e.Position.Latitude,
                e.Position.Longitude,
                String.Format("traveled: {0:0.00} KM",
                distancetraveled), "", 3);
            });
        }

        // Instance method to create a pin placeholder to the custom map
        public void CreatePinPlaceholder(PinType pinType, double latitude,
        double longitude,
        String label,
        String address, int Id)
        {
            customMap.Pins.Add(new Pin
            {
                Type = pinType,
                Position = new Xamarin.Forms.Maps.Position(latitude, longitude),
                Label = label,
                Address = address,
                MarkerId = Id
            });
            // Show the users current location on the map
            customMap.IsShowingUser = true;
        }


        // Instance method that ends the current trail and returns back to the main screen.
        public async void EndThisTrailButton_Clicked(object sender, EventArgs e)
        {
            // Stop listening for location updates prior to navigating
            App.SelectedItem = null;
            _viewModel.OnStopUpdate();
            await _viewModel.Navigation.BackToMainPage();
        }
    }
}
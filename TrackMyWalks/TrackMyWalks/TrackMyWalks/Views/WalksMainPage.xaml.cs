using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Models;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackMyWalks.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalksMainPage : ContentPage
    {

        // Return the Binding Context for the ViewModel
        WalksMainPageViewModel _viewModel => BindingContext as WalksMainPageViewModel;

        public WalksMainPage()
        {
            InitializeComponent();
            Title = "Track My Walks";
            this.Title = "Track My Walks Listing";
            this.BindingContext = new WalksMainPageViewModel();

            //this.InitialiseWalks();
        }

        public void InitialiseWalks()
        {
            // Create a collection that will raise an event,
            // whenever an object is added or removed from
            // our WalksListModel collection.
            var WalksListModel = new ObservableCollection<WalkDataModel>
            {
                // Populate our collection with some dummy data that will be used
                // to populate our ListView
                new WalkDataModel
                {
                    Id = 1,Title = "10 Mile Brook Trail, Margaret River",
                    Description = "The 10 Mile Brook Trail starts in the Rotary Park near Old Kate, a preserved steam engine at the northern edge of Margaret River. ",
                    Latitude = -33.9727604,
                    Longitude = 115.0861599,
                    Distance = 7.5,
                    Difficulty = "Medium",
                    ImageUrl = "https://trailswa.com.au/media/cache/media/images/trails/_mid/FullSizeRender1_600_480_c1.jpg"
                },
                new WalkDataModel
                {
                    Id = 2,
                    Title = "Ancient Empire Walk, Valley of the Giants",
                    Description = "The Ancient Empire is a 450 metre walk trail that takes you around and through some of the giant tingle trees including the most popular of the gnarled veterans, known as Grandma Tingle.",
                    Latitude = -34.9749188,
                    Longitude = 117.3560796,
                    Distance = 450,
                    Difficulty = "Hard",
                    ImageUrl = "https://trailswa.com.au/media/cache/media/images/trails/_mid/Ancient_Empire_534_480_c1.jpg"
                }};
            // Populate our ListView with entries from our WalksListModel
            WalkEntriesListView.ItemsSource = WalksListModel;
        }


        // Instance method to call the WalkEntryPage to add a Walk Entry
        public void AddWalk_Clicked(object sender, EventArgs e)
        {
            App.SelectedItem = null;
            Navigation.PushAsync(new WalkEntryPage());
        }

        // Instance method to call the WalkEntryPage to allow item
        // to be edited
        public async void OnEditItem(object sender, EventArgs e)
        {
            // Get the selected item to be edited from our ListView
            var selectedItem = (WalkDataModel)((MenuItem)sender).CommandParameter;
            App.SelectedItem = selectedItem;
            await Navigation.PushAsync(new WalkEntryPage());
        }

        // Instance method to remove the trail item from our
        // collection
        public async void OnDeleteItem(object sender, EventArgs e)
        {
            // Get the selected item to be deleted from our ListView
            var selectedItem = (WalkDataModel)((MenuItem)sender).CommandParameter;
            // Prompt the user with a confirmation dialog to confirm
            if (await DisplayAlert("Delete Walk Entry Item",
            "Are you sure you want to delete this Walk Entry Item?",
            "OK", "Cancel"))
            {
                // Remove Walk Item from our WalkListModel collection
                _viewModel.WalksListModel.Remove(selectedItem);
            }
            else
                return;
        }

        // Instance method to call the WalkTrailInfoPage using the selected item
        public void myWalkEntries_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Get the selected item from our ListView
            App.SelectedItem = e.Item as WalkDataModel;
            Navigation.PushAsync(new WalkTrailInfoPage());
        }

        // Method to initialise our View Model when the ContentPage
        // appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel != null)
            {
                // Call the Init method to initialise the ViewModel
                await _viewModel.Init();
            }
            // Set up and initialise the binding for our ListView
            WalkEntriesListView.SetBinding(ItemsView<Cell>.ItemsSourceProperty,
            new Binding("."));
            WalkEntriesListView.BindingContext = _viewModel.WalksListModel;
        }
    }
}
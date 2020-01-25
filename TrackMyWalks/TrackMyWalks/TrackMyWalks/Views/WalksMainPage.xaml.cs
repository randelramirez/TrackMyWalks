﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Models;
using TrackMyWalks.Services;
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
            this.Title = "Track My Walks Listing";
            this.BindingContext = new WalksMainPageViewModel(DependencyService.Get<INavigationService>());
        }

        // Instance method to call the WalkEntryPage to add a Walk Entry
        public async void AddWalk_Clicked(object sender, EventArgs e)
        {
            App.SelectedItem = null;
            await _viewModel.Navigation.NavigateTo<WalkEntryPageViewModel>();
        }

        // Instance method to call the WalkEntryPage to allow item to be edited
        public async void OnEditItem(object sender, EventArgs e)
        {
            // Get the selected item to be edited from our ListView
            var selectedItem = (WalkDataModel)((MenuItem)sender).CommandParameter;
            App.SelectedItem = selectedItem;
            await _viewModel.Navigation.NavigateTo<WalkEntryPageViewModel>();
        }

        // Instance method to remove the trail item from our collection
        public async void OnDeleteItem(object sender, EventArgs e)
        {
            // Get the selected item to be deleted from our ListView
            var selectedItem = (WalkDataModel)((MenuItem)sender).CommandParameter;
            // Prompt the user with a confirmation dialog to confirm
            if (await DisplayAlert("Delete Walk Entry Item", "Are you sure you want to delete this Walk Entry Item ? ", "OK", "Cancel"))
            {
                // Remove Walk Item from our WalkListModel collection
                _viewModel.WalksListModel.Remove(selectedItem);
            }
            else
                return;
        }

        // Instance method to call the WalkTrailInfoPage using the selected item
        public async void myWalkEntries_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Get the selected item from our ListView
            App.SelectedItem = e.Item as WalkDataModel;
            await _viewModel.Navigation.NavigateTo<WalkTrailInfoPageViewModel>();
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
            WalkEntriesListView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, new Binding("."));
            WalkEntriesListView.BindingContext = _viewModel.WalksListModel;
        }
    }
}
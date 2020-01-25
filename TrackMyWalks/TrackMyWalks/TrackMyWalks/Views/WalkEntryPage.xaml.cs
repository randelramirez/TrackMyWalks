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
    public partial class WalkEntryPage : ContentPage
    {
        // Return the Binding Context for the ViewModel
        WalkEntryPageViewModel _viewModel => BindingContext as WalkEntryPageViewModel;

        public WalkEntryPage()
        {
            InitializeComponent();
            Title = "New Walk Entry Page";

            // Update the Title and Initialise our BindingContext for the Page
            Title = "New Walk Entry Page";
            BindingContext = new WalkEntryPageViewModel(DependencyService.Get<INavigationService>());
            SetBinding(TitleProperty, new Binding(BaseViewModel.PageTitlePropertyName));

        }

        public async void SaveWalkItem_Clicked(object sender, EventArgs e)
        {
            // Prompt the user with a confirmation dialog to confirm
            if (await DisplayAlert("Save Walk Entry Item", "Proceed and save changes?", "OK", "Cancel"))
            {
                // Attempt to save and validate our Walk Entry Item
                if (!_viewModel.ValidateFormDetailsAndSave())
                    // Error Saving - Must have Title and description
                    await DisplayAlert("Validation Error",
                    "Title and Description are required.", "OK");
                else
                    // Navigate back to the Track My Walks Listing page
                    await _viewModel.Navigation.RemoveViewFromStack();
            }
            else
            {
                // Navigate back to the Track My Walks Listing page
                await _viewModel.Navigation.RemoveViewFromStack();
            }
        }
    }
}
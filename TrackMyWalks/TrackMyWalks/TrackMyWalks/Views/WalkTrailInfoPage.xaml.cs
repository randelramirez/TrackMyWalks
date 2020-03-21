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
            if (App.SelectedItem == null)
                return;
            // Create a Simple Animation to rotate our Begin Trail
            // Walk Button
            //await BeginTrailWalk.RotateTo(360, 1000);
            //BeginTrailWalk.Rotation = 0;

            // Create and Apply an Easing Function to our Button
            await BeginTrailWalk.RotateTo(15, 1000, new Easing(t =>
            Math.Sin(Math.PI * t) *
            Math.Sin(Math.PI * 20 * t)));

            await _viewModel.Navigation.NavigateTo<WalkDistancePageViewModel>();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            #region animation for changing color button
            // Create a Custom Animation for our BeginTrailWalk button
            var animation = new Animation(v =>
                BeginTrailWalk.BackgroundColor = Color.FromHsla(v, 1, 0.5), start: 0, end: 1);
            animation.Commit(this, "BeginWalkCustomAnimation",
                16,
                5000,
                Easing.Linear, (v, c) =>
                BackgroundColor = Color.Default, () => true);
            #endregion

            // Create a SlidingEntrance Animation for WalkTrailInfoPage

            double offset = 1000;
            foreach (View view in TrailInfoScrollView.Children)
            {
                view.TranslationX = offset;
                offset *= -1;
                await Task.WhenAny(view.TranslateTo(0, 0, 1000,
                    Easing.SpringOut), Task.Delay(100));
            }
        }
    }
}
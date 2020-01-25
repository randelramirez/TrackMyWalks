﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Services;

namespace TrackMyWalks.ViewModels
{
    public class WalkTrailInfoPageViewModel : BaseViewModel
    {
        public WalkTrailInfoPageViewModel(INavigationService navService) : base(navService)
        {
        }

        // Update each control on the WalkTrailInfoPage with values from our Model
        public string Title => App.SelectedItem.Title;
        public string Description => App.SelectedItem.Description;
        public double Distance => App.SelectedItem.Distance;
        public String Difficulty => App.SelectedItem.Difficulty;
        public String ImageUrl => App.SelectedItem.ImageUrl;

        // Instance method to initialise the WalkTrailInfoPageViewModel
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
    }
}

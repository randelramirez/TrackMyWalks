using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Services;

namespace TrackMyWalks.ViewModels
{
    public class TwitterSignInPageViewModel : BaseViewModel
    {
        public TwitterSignInPageViewModel(INavigationService navService) : base(navService)
        {
        }

        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
    }
}

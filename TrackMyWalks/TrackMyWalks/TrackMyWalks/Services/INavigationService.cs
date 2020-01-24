using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;

namespace TrackMyWalks.Services
{
    public interface INavigationService
    {
        // Asynchronously removes the most recent page from the navigation stack.
        Task<Page> RemoveViewFromStack();
        // Returns to the Root Page after removing the current page from the
        // navigation stack
        Task BackToMainPage();
        // Navigate to a particular ViewModel within our MVVM Model
        Task NavigateTo<TVM>() where TVM : BaseViewModel;
    }
}

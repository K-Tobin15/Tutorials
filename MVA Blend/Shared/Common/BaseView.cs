using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Shared.Common
{
    public abstract class BaseView : Page
    {
        public BaseViewModel ViewModel { get { return this.DataContext as BaseViewModel; } }
        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.ViewModel.OnNavigatedTo(e);
            base.OnNavigatedTo(e);
        }
        protected override void OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e)
        {
            this.ViewModel.OnNavigatingFrom(e);
            base.OnNavigatingFrom(e);
        }
    }
}

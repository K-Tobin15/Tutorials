using Shared.Common;
using Shared.Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Shared.Common
{
    public abstract class BaseViewModel : BindableBase
    {
        public BaseViewModel()
        {
            // setup orientation
            Windows.UI.Xaml.Window.Current.SizeChanged += (s, e) => UpdateOrientations();
            UpdateOrientations();
        }

        public virtual void OnNavigatedTo(NavigationEventArgs e) { }
        public virtual void OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e) { }

        DelegateCommand<Type> m_GoBackCommand = null;
        public DelegateCommand<Type> GoBackCommand
        {
            get
            {
                if (m_GoBackCommand != null)
                    return m_GoBackCommand;
                m_GoBackCommand = new DelegateCommand<Type>
                (
                    o =>
                    {
                        if (o != null)
                            Shared.Services.NavigationService.Instance.Navigate(o, null);
                        else
                            Shared.Services.NavigationService.Instance.GoBack();
                    },
                    o =>
                    {
                        if (o != null) return true;
                        try { return Shared.Services.NavigationService.Instance.CanGoBack(); }
                        catch { } return false;
                    }
                );
                this.PropertyChanged += (s, e) => m_GoBackCommand.RaiseCanExecuteChanged();
                return m_GoBackCommand;
            }
        }

        public enum Orientations { Horizontal, Vertical, Snap }

        Orientations _Orientation = Orientations.Horizontal;
        public Orientations Orientation { get { return _Orientation; } set { SetProperty(ref _Orientation, value); } }

        private void UpdateOrientations()
        {
            var oldValue = this.Orientation;
            switch (Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Orientation)
            {
                case Windows.UI.ViewManagement.ApplicationViewOrientation.Landscape:
                    this.Orientation = Orientations.Horizontal;
                    break;
                case Windows.UI.ViewManagement.ApplicationViewOrientation.Portrait:
                    this.Orientation = Orientations.Vertical;
                    break;
            }
            if (Windows.UI.Xaml.Window.Current.Bounds.Width == 320)
                this.Orientation = Orientations.Snap;
            if (!oldValue.Equals(this.Orientation))
                RaiseOrientationChanged(oldValue, this.Orientation);
        }

        #region OrientationChanged event

        public class OrientationChangedEventArgs : System.EventArgs
        {
            public Orientations OldValue { get; set; }
            public Orientations NewValue { get; set; }
        }
        public event EventHandler<OrientationChangedEventArgs> OrientationChanged;
        protected void RaiseOrientationChanged(Orientations oldValue, Orientations newValue)
        {
            if (OrientationChanged != null)
                OrientationChanged(this, new OrientationChangedEventArgs
                {
                    OldValue = oldValue,
                    NewValue = newValue
                });
        }

        #endregion
    }
}

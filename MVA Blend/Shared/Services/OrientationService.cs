using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public sealed class OrientationService : IService
    {
        private OrientationService() { Setup(); }

        private static OrientationService _Instance;
        public static OrientationService Instance
        {
            get { return _Instance ?? (_Instance = new OrientationService()); }
        }

        public void Setup()
        {
            Cleanup();
            Windows.UI.Xaml.Window.Current.SizeChanged += Current_SizeChanged;
        }

        public void Cleanup()
        {
            Windows.UI.Xaml.Window.Current.SizeChanged -= Current_SizeChanged;
        }

        public enum States { Horizontal, Vertical, Snap }
        public Action<States> OrientationChanged { get; set; }
        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            try
            {
                States state = default(States);
                switch (Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Orientation)
                {
                    case Windows.UI.ViewManagement.ApplicationViewOrientation.Landscape:
                        state = States.Horizontal;
                        break;
                    case Windows.UI.ViewManagement.ApplicationViewOrientation.Portrait:
                        state = States.Vertical;
                        break;
                }
                if (e.Size.Width < 500)
                    state = States.Vertical & States.Snap;
                OrientationChanged(state);
            }
            catch { }
        }
    }
}

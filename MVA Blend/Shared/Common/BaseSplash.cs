using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Shared.Common
{
    public abstract class BaseSplash : Page, ISplash
    {
        public Windows.ApplicationModel.Activation.SplashScreen SplashScreen { get; set; }
        public Action Navigate { get; set; }
        public async void Start()
        {
            // display splash when ready
            SplashImage.ImageOpened += (s, e) => Window.Current.Activate();

            // setup size
            Resize();
            Window.Current.SizeChanged += (s, e) => Resize();

            // start
            Window.Current.Activate();
            await LoadThings();
            this.Navigate.Invoke();
        }

        private void Resize()
        {
            SplashImage.Height = this.SplashScreen.ImageLocation.Height;
            SplashImage.Width = this.SplashScreen.ImageLocation.Width;
            SplashImage.SetValue(Canvas.TopProperty, this.SplashScreen.ImageLocation.Top);
            SplashImage.SetValue(Canvas.LeftProperty, this.SplashScreen.ImageLocation.Left);
        }

        public abstract Image SplashImage { get; }
        public abstract Task LoadThings();
    }
}

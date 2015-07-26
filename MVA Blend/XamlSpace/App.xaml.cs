using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace XamlSpace
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        public static Services.NavigationService NavigationService = new Services.NavigationService(App.Current);
        protected override void OnLaunched(LaunchActivatedEventArgs args) { NavigationService.Activate(args as IActivatedEventArgs); }
        protected override void OnActivated(IActivatedEventArgs args) { NavigationService.Activate(args as IActivatedEventArgs); }
        protected override void OnSearchActivated(SearchActivatedEventArgs args) { NavigationService.Activate(args as IActivatedEventArgs); }
    }
}

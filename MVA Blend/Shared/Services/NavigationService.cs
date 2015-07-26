using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    using Shared.Interfaces;
    using Shared.Microsoft;
    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public abstract class NavigationService : Shared.Interfaces.INavigationService
    {
        internal NavigationService()
        {
            Setup();
            Instance = this;
        }
        protected NavigationService(Application app)
            : this()
        {
            app.Suspending += (s, e) => this.Suspending(e);
            app.Resuming += (s, e) => this.Resuming(e);
        }

        internal static NavigationService Instance { get; private set; }
        public virtual void Setup() { Cleanup(); }
        public virtual void Cleanup() { }

        public virtual void Activate(IActivatedEventArgs e)
        {
            switch (e.Kind)
            {
                case ActivationKind.Launch:
                    {
                        var args = e as ILaunchActivatedEventArgs;
                        if (args.TileId.Equals("App"))
                            GotoSplash(args, () => GotoMainPage());
                        else // secondary tile
                            GotoSplash(args, () => GotoSecondaryTile(args));
                        break;
                    }
                case ActivationKind.Protocol:
                    {
                        var args = e as IProtocolActivatedEventArgs;
                        GotoSplash(e, () => GotoProtocolActivation(args));
                        break;
                    }
                case ActivationKind.Search:
                    {
                        var args = e as ISearchActivatedEventArgs;
                        GotoSplash(e, () => GotoSearch(args));
                        break;
                    }
                default:
                    throw new NotImplementedException(e.Kind.ToString());
            }
        }

        // these must be overridden
        public abstract void GotoMainPage();
        public abstract void GotoSplash(IActivatedEventArgs args, Action navigate);

        // this might be overridden
        public virtual void GotoSearch(ISearchActivatedEventArgs args) { /* TODO */ }
        public virtual void GotoProtocolActivation(IProtocolActivatedEventArgs args) { /* TODO */ }
        public virtual void GotoSecondaryTile(ILaunchActivatedEventArgs args) { /* TODO */ }

        // these aren't tested
        public async void Suspending(Windows.ApplicationModel.SuspendingEventArgs e) { await SuspensionManager.SaveAsync(); }
        public async void Resuming(object e) { await SuspensionManager.RestoreAsync(); }

        protected Frame Frame
        {
            get
            {
                Frame rootFrame = Window.Current.Content as Frame;
                if (rootFrame == null)
                {
                    rootFrame = new Frame();
                    Window.Current.Content = rootFrame;
                }
                return rootFrame;
            }
        }

        public class NavigatingEventArgs : System.EventArgs
        {
            public bool Cancel { get; set; }
            public Type Destination { get; set; }
        }
        public event EventHandler Navigating;
        protected bool RaiseNavigating(Type type)
        {
            var _Args = new NavigatingEventArgs { Cancel = false, Destination = type };
            if (Navigating != null)
                Navigating(null, _Args);
            return _Args.Cancel;
        }

        public bool Navigate(Type type, object arg)
        {
            if (RaiseNavigating(type))
                return false;
            return Frame.Navigate(type, arg);
        }

        public bool CanGoBack()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return true;
            try { return Frame.CanGoBack; }
            catch { } return false;
        }

        public void GoBack()
        {
            if (CanGoBack() && !RaiseNavigating(null))
                Frame.GoBack();
        }

        protected void GotoSplash<T>(IActivatedEventArgs args, Action navigate) where T : ISplash, new()
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                navigate.Invoke();
            else
            {
                // do not activate, let the splash activate itself
                var page = new T() { SplashScreen = args.SplashScreen, Navigate = navigate };
                Window.Current.Content = page as UIElement;
                page.Start();
            }
        }
    }
}

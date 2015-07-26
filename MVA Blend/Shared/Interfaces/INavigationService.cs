using System;
namespace Shared.Interfaces
{
    public interface INavigationService : IService
    {
        bool CanGoBack();
        void GoBack();
        void GotoMainPage();
        void GotoSplash(global::Windows.ApplicationModel.Activation.IActivatedEventArgs args, Action navigate);
        event EventHandler Navigating;
    }
}

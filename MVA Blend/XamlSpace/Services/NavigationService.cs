using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace XamlSpace.Services
{
    public class NavigationService : Shared.Services.NavigationService
    {
        public NavigationService(Windows.UI.Xaml.Application app) : base(app) { }
        public override void GotoMainPage() { Navigate(typeof(Views.MainPage), null); }
        public override void GotoSplash(IActivatedEventArgs args, Action navigate) { base.GotoSplash<Views.Splash>(args, navigate); }
        public void GotoDetail(Shared.SampleData.StarTrek.Character character) { base.Navigate(typeof(Views.Detail), character); }
    }
}
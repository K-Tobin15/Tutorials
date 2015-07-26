using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace XamlWatch.Views
{
    public sealed partial class Splash : Shared.Common.BaseSplash
    {
        public Splash()
        {
            this.InitializeComponent();
        }

        public override Image SplashImage
        {
            get { return this.MyImage; }
        }

        public override async Task LoadThings()
        {
            await Task.Delay(0);
        }
    }
}

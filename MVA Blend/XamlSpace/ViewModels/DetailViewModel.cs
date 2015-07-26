using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlSpace.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        Shared.SampleData.StarTrek.Character _Character = default(Shared.SampleData.StarTrek.Character);
        public Shared.SampleData.StarTrek.Character Character { get { return _Character; } set { SetProperty(ref _Character, value); } }

        public override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.Character = e.Parameter as Shared.SampleData.StarTrek.Character;
            base.OnNavigatedTo(e);
        }
    }
}

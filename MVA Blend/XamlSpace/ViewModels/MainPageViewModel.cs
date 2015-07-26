using Shared.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace XamlSpace.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            SetupCommand.Execute(null);
        }

        public override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.OrientationChanged += MainPageViewModel_OrientationChanged;
            this.PropertyChanged += MainPageViewModel_PropertyChanged;
            base.OnNavigatedTo(e);
        }

        public override void OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e)
        {
            this.OrientationChanged -= MainPageViewModel_OrientationChanged;
            this.PropertyChanged -= MainPageViewModel_PropertyChanged;
            base.OnNavigatingFrom(e);
        }

        void MainPageViewModel_OrientationChanged(object sender, BaseViewModel.OrientationChangedEventArgs e) { LoadData(); }

        void MainPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals("SelectedCharacter"))
                return;
            if (SelectedCharacter != null)
                App.NavigationService.GotoDetail(this.SelectedCharacter);
        }

        string _Title = default(string);
        public string Title { get { return _Title; } set { SetProperty(ref _Title, value); } }

        ObservableCollection<Shared.SampleData.StarTrek.Character> _EntCharacters = new ObservableCollection<Shared.SampleData.StarTrek.Character>();
        public ObservableCollection<Shared.SampleData.StarTrek.Character> EntCharacters { get { return _EntCharacters; } }

        ObservableCollection<Shared.SampleData.StarTrek.Character> _TngCharacters = new ObservableCollection<Shared.SampleData.StarTrek.Character>();
        public ObservableCollection<Shared.SampleData.StarTrek.Character> TngCharacters { get { return _TngCharacters; } }

        ObservableCollection<Shared.SampleData.StarTrek.Character> _TosCharacters = new ObservableCollection<Shared.SampleData.StarTrek.Character>();
        public ObservableCollection<Shared.SampleData.StarTrek.Character> TosCharacters { get { return _TosCharacters; } }

        Shared.SampleData.StarTrek.Character _SelectedCharacter = default(Shared.SampleData.StarTrek.Character);
        public Shared.SampleData.StarTrek.Character SelectedCharacter { get { return _SelectedCharacter; } set { SetProperty(ref _SelectedCharacter, value); } }

        DelegateCommand<object> m_SetupCommand = null;
        public DelegateCommand<object> SetupCommand
        {
            get
            {
                if (m_SetupCommand != null)
                    return m_SetupCommand;
                m_SetupCommand = new DelegateCommand<object>
                (
                    o => LoadData(),
                    o => true
                );
                this.PropertyChanged += (s, e) => m_SetupCommand.RaiseCanExecuteChanged();
                return m_SetupCommand;
            }
        }

        void LoadData()
        {
            this.Title = "XAML Space";

            // fetch the data
            var series = Shared.SampleData.StarTrek.Context.Series(true, false);
            var ent = series.First(x => x.Code == "ENT");
            var tng = series.First(x => x.Code == "TNG");
            var tos = series.First(x => x.Code == "TOS");

            // rearrange the data
            switch (this.Orientation)
            {
                case Orientations.Horizontal:
                    TosMax = Shared.SampleData.StarTrek.Context.Arrange(tos.Characters, Shared.SampleData.StarTrek.Context.Layouts.WideStyle1With8);
                    TngMax = Shared.SampleData.StarTrek.Context.Arrange(tng.Characters, Shared.SampleData.StarTrek.Context.Layouts.WideStyle1With7);
                    EntMax = Shared.SampleData.StarTrek.Context.Arrange(ent.Characters, Shared.SampleData.StarTrek.Context.Layouts.WideStyle2With7);
                    break;
                case Orientations.Vertical:
                    TosMax = Shared.SampleData.StarTrek.Context.Arrange(tos.Characters, Shared.SampleData.StarTrek.Context.Layouts.NarrowStyle1With8);
                    TngMax = Shared.SampleData.StarTrek.Context.Arrange(tng.Characters, Shared.SampleData.StarTrek.Context.Layouts.NarrowStyle1With7);
                    EntMax = Shared.SampleData.StarTrek.Context.Arrange(ent.Characters, Shared.SampleData.StarTrek.Context.Layouts.NarrowStyle2With7);
                    break;
                case Orientations.Snap:
                    TosMax = Shared.SampleData.StarTrek.Context.Arrange(tos.Characters, Shared.SampleData.StarTrek.Context.Layouts.Snap);
                    TngMax = Shared.SampleData.StarTrek.Context.Arrange(tng.Characters, Shared.SampleData.StarTrek.Context.Layouts.Snap);
                    EntMax = Shared.SampleData.StarTrek.Context.Arrange(ent.Characters, Shared.SampleData.StarTrek.Context.Layouts.Snap);
                    break;
            }

            // clear any existing
            this.EntCharacters.Clear();
            this.TngCharacters.Clear();
            this.TosCharacters.Clear();

            // updat the collections
            foreach (var item in ent.Characters.Select((x, i) => new { Index = i, Item = x }))
                this.EntCharacters.Add(item.Item);
            foreach (var item in tng.Characters.Select((x, i) => new { Index = i, Item = x }))
                this.TngCharacters.Add(item.Item);
            foreach (var item in tos.Characters.Select((x, i) => new { Index = i, Item = x }))
                this.TosCharacters.Add(item.Item);
        }

        // vriable sezed wrap grid

        int _TosMax = default(int);
        public int TosMax { get { return _TosMax; } set { SetProperty(ref _TosMax, value); } }

        int _TngMax = default(int);
        public int TngMax { get { return _TngMax; } set { SetProperty(ref _TngMax, value); } }

        int _EntMax = default(int);
        public int EntMax { get { return _EntMax; } set { SetProperty(ref _EntMax, value); } }
    }
}

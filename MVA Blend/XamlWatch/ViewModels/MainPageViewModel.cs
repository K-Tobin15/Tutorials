using Shared.Common;
using XamlWatch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace XamlWatch.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            //if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            //{
            //    this._Colors = new ObservableCollection<ColorModel>();
            //    return;  // design only
            //}

            var timer = new Windows.UI.Xaml.DispatcherTimer { Interval = TimeSpan.FromSeconds(.1) };
            timer.Tick += (s, e) =>
            {
                this.SecondsAngle = ((double)DateTime.Now.Millisecond / 1000f + (double)DateTime.Now.Second) * 6d;
                this.MinutesAngle = DateTime.Now.Minute * 6;
                this.HoursAngle = DateTime.Now.Hour * 30;
                this.Day = DateTime.Now.Day.ToString();
            };
            timer.Start();
            ResetCommand.Execute(null);

            // listen for purchase
            Shared.Services.MessageBus.Instance.Register<MainPageViewModel>((m, p) =>
            {
                if (!m.Equals("PURCHASE"))
                    return;
                foreach (var item in Purchases.Where(x => x.Bitmap == null).ToArray())
                    Purchases.Remove(item);
                this.Purchases.Insert(0, p as PurchaseModel);
            });
        }

        double _SecondsAngle = default(double);
        public double SecondsAngle { get { return _SecondsAngle; } set { SetProperty(ref _SecondsAngle, value); } }

        double _MinutesAngle = default(double);
        public double MinutesAngle { get { return _MinutesAngle; } set { SetProperty(ref _MinutesAngle, value); } }

        double _HoursAngle = default(double);
        public double HoursAngle { get { return _HoursAngle; } set { SetProperty(ref _HoursAngle, value); } }

        string _Day = default(string);
        public string Day { get { return _Day; } set { SetProperty(ref _Day, value); } }

        public object[] Ticks { get { return Enumerable.Range(1, 60).Select(x => new { Angle = x * 6 }).ToArray(); } }

        ObservableCollection<ColorModel> _Colors = new ObservableCollection<ColorModel>(new[]{
            // sample data  
             new {Color = Windows.UI.ColorHelper.FromArgb(255, 22, 197, 254), Name = "Windows"} 
            ,new {Color = Windows.UI.Colors.DarkGray, Name = "DarkGray"}
            ,new {Color = Windows.UI.Colors.Red, Name = "Red"}
            ,new {Color = Windows.UI.Colors.Blue, Name = "Blue"}
            ,new {Color = Windows.UI.Colors.Green, Name = "Green"}
            ,new {Color = Windows.UI.Colors.Goldenrod, Name = "Goldenrod"}
            ,new {Color = Windows.UI.Colors.Black, Name = "Black"}
            ,new {Color = Windows.UI.Colors.DarkRed, Name = "DarkRed"}
            ,new {Color = Windows.UI.Colors.DarkBlue, Name = "DarkBlue"}
            ,new {Color = Windows.UI.Colors.DarkGreen, Name = "DarkGreen"}
            ,new {Color = Windows.UI.Colors.DarkGoldenrod, Name = "DarkGoldenrod"}}
            .Select((x, i) => new ColorModel(x.Color, (i == 0) ? 2 : 1) { Name = x.Name }));
        public ObservableCollection<ColorModel> Colors { get { return _Colors; } }

        ColorModel _SelectedColor = default(ColorModel);
        public ColorModel SelectedColor { get { return _SelectedColor; } set { SetProperty(ref _SelectedColor, value); } }

        ObservableCollection<FaceModel> _Faces = new ObservableCollection<FaceModel>(
            // sample data
            Enumerable.Range(0, 9).Select(x => new FaceModel(string.Format("/Images/face{0}.jpg", x))));
        public ObservableCollection<FaceModel> Faces { get { return _Faces; } }

        FaceModel _SelectedFace = default(FaceModel);
        public FaceModel SelectedFace { get { return _SelectedFace; } set { SetProperty(ref _SelectedFace, value); } }

        ObservableCollection<PurchaseModel> _Purchases = new ObservableCollection<PurchaseModel>(
            // sample data
            Enumerable.Range(1, 3).Select(x => new PurchaseModel()));
        public ObservableCollection<PurchaseModel> Purchases { get { return _Purchases; } }

        DelegateCommand<RenderTargetBitmap> m_PurchaseCommand = null;
        public DelegateCommand<RenderTargetBitmap> PurchaseCommand
        {
            get
            {
                if (m_PurchaseCommand != null)
                    return m_PurchaseCommand;
                m_PurchaseCommand = new DelegateCommand<RenderTargetBitmap>
                (
                    o =>
                    {
                        var p = new Views.Purchase { DataContext = new ViewModels.PurchaseViewModel { Bitmap = o } };
                        p.Show();
                    },
                    o => true
                );
                this.PropertyChanged += (s, e) => m_PurchaseCommand.RaiseCanExecuteChanged();
                return m_PurchaseCommand;
            }
        }

        DelegateCommand<object> m_ResetCommand = null;
        public DelegateCommand<object> ResetCommand
        {
            get
            {
                if (m_ResetCommand != null)
                    return m_ResetCommand;
                m_ResetCommand = new DelegateCommand<object>
                (
                    o =>
                    {
                        this.SelectedColor = this.Colors.First();
                        this.SelectedFace = this.Faces.First();
                    },
                    o => true
                );
                this.PropertyChanged += (s, e) => m_ResetCommand.RaiseCanExecuteChanged();
                return m_ResetCommand;
            }
        }
    }
}

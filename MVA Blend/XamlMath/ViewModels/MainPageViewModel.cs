using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;
using Shared.Controls;
using Shared.Common;

namespace XamlMath.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            Dividend = 10;
            Divisor = 2;

            Multiplicand = 10;
            Multiplier = 2;

            Minuend = 10;
            Subtrahend = 2;

            Augend = 10;
            Addend = 2;
        }

        public override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs param)
        {
            Shared.Services.Contracts.SettingsService.Instance.SettingsRequested = (o) =>
            {
                o.Request.ApplicationCommands.Add(new Windows.UI.ApplicationSettings.SettingsCommand("About", "About", (e) =>
                {
                    var about = new Views.About();
                    about.Show();
                }));
            };
            base.OnNavigatedTo(param);
        }

        #region division

        double _Dividend = default(double);
        public double Dividend { get { return _Dividend; } set { SetProperty(ref _Dividend, value); } }

        double _Divisor = default(double);
        public double Divisor { get { return _Divisor; } set { SetProperty(ref _Divisor, value); } }

        double _Quotient = default(double);
        public double Quotient { get { return _Quotient; } set { SetProperty(ref _Quotient, value); } }

        DelegateCommand<object> m_DivideCommand = null;
        public DelegateCommand<object> DivideCommand
        {
            get
            {
                if (m_DivideCommand != null)
                    return m_DivideCommand;
                m_DivideCommand = new DelegateCommand<object>
                (
                    o => { Quotient = Dividend / Divisor; },
                    o => { return Divisor != 0; }
                );
                this.PropertyChanged += (s, e) => m_DivideCommand.RaiseCanExecuteChanged();
                return m_DivideCommand;
            }
        }

        #endregion

        #region multiplication

        double _Multiplicand = default(double);
        public double Multiplicand { get { return _Multiplicand; } set { SetProperty(ref _Multiplicand, value); } }

        double _Multiplier = default(double);
        public double Multiplier { get { return _Multiplier; } set { SetProperty(ref _Multiplier, value); } }

        double _Product = default(double);
        public double Product { get { return _Product; } set { SetProperty(ref _Product, value); } }

        DelegateCommand<object> m_MultiplyCommand = null;
        public DelegateCommand<object> MultiplyCommand
        {
            get
            {
                if (m_MultiplyCommand != null)
                    return m_MultiplyCommand;
                m_MultiplyCommand = new DelegateCommand<object>
                (
                    o => { Product = Multiplicand * Multiplier; },
                    o => true
                );
                this.PropertyChanged += (s, e) => m_MultiplyCommand.RaiseCanExecuteChanged();
                return m_MultiplyCommand;
            }
        }

        #endregion

        #region subtraction

        double _Minuend = default(double);
        public double Minuend { get { return _Minuend; } set { SetProperty(ref _Minuend, value); } }

        double _Subtrahend = default(double);
        public double Subtrahend { get { return _Subtrahend; } set { SetProperty(ref _Subtrahend, value); } }

        double _Difference = default(double);
        public double Difference { get { return _Difference; } set { SetProperty(ref _Difference, value); } }

        DelegateCommand<object> m_SubtractCommand = null;
        public DelegateCommand<object> SubtractCommand
        {
            get
            {
                if (m_SubtractCommand != null)
                    return m_SubtractCommand;
                m_SubtractCommand = new DelegateCommand<object>
                (
                    o => { Difference = Minuend - Subtrahend; },
                    o => true
                );
                this.PropertyChanged += (s, e) => m_SubtractCommand.RaiseCanExecuteChanged();
                return m_SubtractCommand;
            }
        }

        #endregion

        #region addition

        double _Augend = default(double);
        public double Augend { get { return _Augend; } set { SetProperty(ref _Augend, value); } }

        double _Addend = default(double);
        public double Addend { get { return _Addend; } set { SetProperty(ref _Addend, value); } }

        double _Sum = default(double);
        public double Sum { get { return _Sum; } set { SetProperty(ref _Sum, value); } }

        DelegateCommand<object> m_AddCommand = null;
        public DelegateCommand<object> AddCommand
        {
            get
            {
                if (m_AddCommand != null)
                    return m_AddCommand;
                m_AddCommand = new DelegateCommand<object>
                (
                    o => { Sum = Augend + Addend; },
                    o => true
                );
                this.PropertyChanged += (s, e) => m_AddCommand.RaiseCanExecuteChanged();
                return m_AddCommand;
            }
        }

        #endregion

        DelegateCommand<object> m_RunAllCommand = null;
        public DelegateCommand<object> RunAllCommand
        {
            get
            {
                if (m_RunAllCommand != null)
                    return m_RunAllCommand;
                m_RunAllCommand = new DelegateCommand<object>
                (
                    o =>
                    {
                        AddCommand.Execute(o);
                        SubtractCommand.Execute(o);
                        MultiplyCommand.Execute(o);
                        DivideCommand.Execute(o);
                    },
                    o => true
                );
                this.PropertyChanged += (s, e) => m_RunAllCommand.RaiseCanExecuteChanged();
                return m_RunAllCommand;
            }
        }
    }
}

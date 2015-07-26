using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Behaviors.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        Shared.Microsoft.RelayCommand<object> _LoadDataCommand = null;
        public Shared.Microsoft.RelayCommand<object> LoadDataCommand
        {
            get
            {
                if (_LoadDataCommand != null)
                    return _LoadDataCommand;
                _LoadDataCommand = new Shared.Microsoft.RelayCommand<object>
                (
                    o => { LoadDataText = string.Format("Data Loaded {0}", DateTime.Now); },
                    o => true
                );
                this.PropertyChanged += (s, e) => _LoadDataCommand.RaiseCanExecuteChanged();
                return _LoadDataCommand;
            }
        }
        string _LoadDataText = default(string);
        public string LoadDataText { get { return _LoadDataText; } set { SetProperty(ref _LoadDataText, value); } }

        public void ReloadData() { ReloadDataText = string.Format("Data Reloaded {0}", DateTime.Now); }
        string _ReloadDataText = default(string);
        public string ReloadDataText { get { return _ReloadDataText; } set { SetProperty(ref _ReloadDataText, value); } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using System.Reflection;
using Module05_Complete.Models;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.ApplicationSettings;

namespace Module05_Complete.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public MainPageViewModel()
        {
            #region SampleData
            var colors = typeof(Colors)
                    .GetRuntimeProperties()
                    .Select(x => new ColorInfo
                    {
                        Name = x.Name,
                        Color = (Color)x.GetValue(null)
                    });
            this.Groups.Add(new GroupInfo { Name = "Group 1", Colors = colors.Skip(00).Take(5).ToArray() });
            this.Groups.Add(new GroupInfo { Name = "Group 2", Colors = colors.Skip(05).Take(5).ToArray() });
            this.Groups.Add(new GroupInfo { Name = "Group 3", Colors = colors.Skip(10).Take(5).ToArray() });
            this.Groups.Add(new GroupInfo { Name = "Group 4", Colors = colors.Skip(15).Take(5).ToArray() });
            this.Groups.Add(new GroupInfo { Name = "Group 5", Colors = colors.Skip(20).Take(5).ToArray() });
            foreach (var item in this.Groups.First().Colors)
                Recent.Add(item);
            #endregion

            // show settings
            var v = Windows.UI.ApplicationSettings.SettingsPane.GetForCurrentView();
            v.CommandsRequested += (s, e) =>
            {
                var c = new SettingsCommand("1", "Options", o => { new Settings().Show(); });
                e.Request.ApplicationCommands.Add(c);
            };

            // respond
            Settings.ClearRecent += async (s, e) =>
            {
                this.Recent.Clear();
                await new MessageDialog("Recent cleared").ShowAsync();
            };
        }

        ColorInfo _Selected = default(ColorInfo);
        public ColorInfo Selected
        {
            get { return _Selected; }
            set
            {
                SetProperty(ref _Selected, value);
                if (!Recent.Contains(value))
                    Recent.Insert(0, value);
                foreach (var item in this.Recent.Skip(5))
                    this.Recent.Remove(item);
            }
        }

        ObservableCollection<ColorInfo> _Recent = new ObservableCollection<ColorInfo>();
        public ObservableCollection<ColorInfo> Recent { get { return _Recent; } }

        ObservableCollection<GroupInfo> _Groups = new ObservableCollection<GroupInfo>();
        public ObservableCollection<GroupInfo> Groups { get { return _Groups; } }
    }

    public abstract class BindableBase : System.ComponentModel.INotifyPropertyChanged
    {

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void SetProperty<T>(ref T storage, T value, [System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (!object.Equals(storage, value))
            {
                storage = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        protected void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Windows.UI;
using Module05_Incomplete.Models;
using Windows.UI.Popups;
using Windows.UI.ApplicationSettings;

namespace Module05_Incomplete.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public MainPageViewModel()
        {
            var colors = typeof(Colors)
                .GetRuntimeProperties().Select(x => new Models.ColorInfo
                {
                    Name = x.Name,
                    Color = (Color)x.GetValue(null)
                });
            this.Groups.Add(new GroupInfo { Name = "Group 1", Colors = colors.Skip(00).Take(5).ToArray() });
            this.Groups.Add(new GroupInfo { Name = "Group 2", Colors = colors.Skip(05).Take(5).ToArray() });
            this.Groups.Add(new GroupInfo { Name = "Group 3", Colors = colors.Skip(10).Take(5).ToArray() });
            this.Groups.Add(new GroupInfo { Name = "Group 4", Colors = colors.Skip(15).Take(5).ToArray() });
            this.Groups.Add(new GroupInfo { Name = "Group 5", Colors = colors.Skip(20).Take(5).ToArray() });

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                foreach (var item in colors.Take(5))
                    this.Recent.Add(item);

            Settings.ClearRecent += async (s, e) =>
            {
                await new MessageDialog("Recent has been cleared").ShowAsync();
                Recent.Clear();
            };

            SettingsPane.GetForCurrentView().CommandsRequested += (s, e) =>
            {
                var c = new SettingsCommand("1", "Options", o =>
                {
                    new Settings().Show();
                });
                e.Request.ApplicationCommands.Add(c);
            };
        }

        ObservableCollection<Models.GroupInfo> _Groups = new ObservableCollection<Models.GroupInfo>();
        public ObservableCollection<Models.GroupInfo> Groups { get { return _Groups; } }

        ColorInfo _Selected = default(ColorInfo);
        public ColorInfo Selected
        {
            get { return _Selected; }
            set
            {
                SetProperty(ref _Selected, value);
                if (Recent.Contains(value))
                    return;
                Recent.Add(value);
                foreach (var item in Recent.Reverse().Skip(5))
                    Recent.Remove(item);
            }
        }

        ObservableCollection<ColorInfo> _Recent = new ObservableCollection<ColorInfo>();
        public ObservableCollection<ColorInfo> Recent { get { return _Recent; } }
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

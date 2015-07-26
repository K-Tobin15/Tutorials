using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Contracts
{
    public sealed class SettingsService : IService
    {
        private SettingsService() { Setup(); }

        private static SettingsService _Instance;
        public static SettingsService Instance
        {
            get { return _Instance ?? (_Instance = new SettingsService()); }
        }
        
        public void Setup()
        {
            Cleanup();
            Windows.UI.ApplicationSettings.SettingsPane
                .GetForCurrentView().CommandsRequested += CommandsRequested;
        }

        public void Cleanup()
        {
            Windows.UI.ApplicationSettings.SettingsPane
                .GetForCurrentView().CommandsRequested -= CommandsRequested;
        }

        public Action<Windows.UI.ApplicationSettings.SettingsPaneCommandsRequestedEventArgs> SettingsRequested { get; set; }
        void CommandsRequested(Windows.UI.ApplicationSettings.SettingsPane sender, Windows.UI.ApplicationSettings.SettingsPaneCommandsRequestedEventArgs args)
        {
            try { SettingsRequested(args); }
            finally { /* nothing */ }
        }

        public void ShowUI()
        {
            Windows.UI.ApplicationSettings.SettingsPane.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public static class Locator<N> where N : Shared.Interfaces.INavigationService, new()
    {
        private static N _NavigationService;
        public static N Navigation
        {
            set { _NavigationService = value; }
            get
            {
                if (_NavigationService != null)
                    return _NavigationService;
                _NavigationService = new N();
                _NavigationService.Setup();
                return _NavigationService;
            }
        }

        private static Contracts.SettingsService _SettingsService;
        public static Contracts.SettingsService Settings
        {
            set { _SettingsService = value; }
            get
            {
                if (_SettingsService != null)
                    return _SettingsService;
                _SettingsService = new Contracts.SettingsService();
                _SettingsService.Setup();
                return _SettingsService;
            }
        }

        private static Contracts.ShareService _ShareService;
        public static Contracts.ShareService Share
        {
            set { _ShareService = value; }
            get
            {
                if (_ShareService != null)
                    return _ShareService;
                _ShareService = new Contracts.ShareService();
                _ShareService.Setup();
                return _ShareService;
            }
        }

        private static Contracts.SearchService _SearchService;
        public static Contracts.SearchService Search
        {
            set { _SearchService = value; }
            get
            {
                if (_SearchService != null)
                    return _SearchService;
                _SearchService = new Contracts.SearchService();
                _SearchService.Setup();
                return _SearchService;
            }
        }

        private static NetworkService _NetworkService;
        public static NetworkService Network
        {
            set { _NetworkService = value; }
            get
            {
                if (_NetworkService != null)
                    return _NetworkService;
                _NetworkService = new NetworkService();
                _NetworkService.Setup();
                return _NetworkService;
            }
        }

        private static SecondaryTileService _SecondaryTileService;
        public static SecondaryTileService SecondaryTile
        {
            set { _SecondaryTileService = value; }
            get
            {
                if (_SecondaryTileService != null)
                    return _SecondaryTileService;
                _SecondaryTileService = new SecondaryTileService();
                _SecondaryTileService.Setup();
                return _SecondaryTileService;
            }
        }
    }
}

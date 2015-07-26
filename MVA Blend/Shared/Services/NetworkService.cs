using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public sealed class NetworkService : IService
    {
        private NetworkService() { Setup(); }

        private static NetworkService _Instance;
        public static NetworkService Instance
        {
            get { return _Instance ?? (_Instance = new NetworkService()); }
        }
        
        public void Setup()
        {
            Cleanup(); 
            Windows.Networking.Connectivity.NetworkInformation
                .NetworkStatusChanged += NetworkStatusChanged;
        }

        public void Cleanup()
        {
            Windows.Networking.Connectivity.NetworkInformation
                .NetworkStatusChanged -= NetworkStatusChanged;
        }

        public Action<bool> NetworkChanged { get; set; }
        async void NetworkStatusChanged(object sender)
        {
            try { NetworkChanged(await this.HasInternet()); }
            catch { }
        }

        public async Task<bool> HasInternet()
        {
            await Task.Delay(0);
            var _Profile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            if (_Profile == null)
                return false;
            var net = Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess;
            return _Profile.GetNetworkConnectivityLevel().Equals(net);
        }
    }
}

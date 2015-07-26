using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace Shared.Services.Contracts
{
    public sealed class ShareService : IService
    {
        private ShareService() { Setup(); }

        private static ShareService _Instance;
        public static ShareService Instance
        {
            get { return _Instance ?? (_Instance = new ShareService()); }
        }

        public void Setup()
        {
            Cleanup();
            Windows.ApplicationModel.DataTransfer.DataTransferManager
                .GetForCurrentView().DataRequested += DataRequested;
        }

        public void Cleanup()
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager
                .GetForCurrentView().DataRequested -= DataRequested;
        }

        public Action<DataRequestedEventArgs> ShareRequested { get; set; }
        void DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var d = args.Request.GetDeferral();
            try { ShareRequested(args); }
            finally { d.Complete(); }
        }

        public void ShowUI()
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }
    }
}

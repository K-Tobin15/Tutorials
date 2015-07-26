using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace XamlWatch.ViewModels
{
    public class PurchaseViewModel : BaseModel
    {
        string _NameOnCard = "Jerry Nixon";
        public string NameOnCard { get { return _NameOnCard; } set { SetProperty(ref _NameOnCard, value); } }

        string _CardNumber = "4321-2345-3456-4567";
        public string CardNumber { get { return _CardNumber; } set { SetProperty(ref _CardNumber, value); } }

        DateTimeOffset _ExpirationDate = DateTimeOffset.Now.AddYears(2);
        public DateTimeOffset ExpirationDate { get { return _ExpirationDate; } set { SetProperty(ref _ExpirationDate, value); } }

        RenderTargetBitmap _Bitmap = default(RenderTargetBitmap);
        public RenderTargetBitmap Bitmap { get { return _Bitmap; } set { SetProperty(ref _Bitmap, value); } }

        string _Street = "1234 West Main Street";
        public string Street { get { return _Street; } set { SetProperty(ref _Street, value); } }

        string _City = "Denver";
        public string City { get { return _City; } set { SetProperty(ref _City, value); } }

        string _State = "CO";
        public string State { get { return _State; } set { SetProperty(ref _State, value); } }

        string _Zip = "80202";
        public string Zip { get { return _Zip; } set { SetProperty(ref _Zip, value); } }

        DelegateCommand<object> m_PurchaseCommand = null;
        public DelegateCommand<object> PurchaseCommand
        {
            get
            {
                if (m_PurchaseCommand != null)
                    return m_PurchaseCommand;
                m_PurchaseCommand = new DelegateCommand<object>
                (
                    async o =>
                    {
                        Shared.Services.MessageBus.Instance.Send("PURCHASE", new Models.PurchaseModel
                        {
                            Date = DateTime.Now,
                            Bitmap = this.Bitmap
                        });
                        var d = new MessageDialog("Thank you for your purchase.", "Purchase Complete");
                        await d.ShowAsync();
                    },
                    o => true
                );
                this.PropertyChanged += (s, e) => m_PurchaseCommand.RaiseCanExecuteChanged();
                return m_PurchaseCommand;
            }
        }
    }
}

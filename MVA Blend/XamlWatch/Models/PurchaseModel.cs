using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace XamlWatch.Models
{
    public class PurchaseModel : Shared.Common.BaseModel
    {
        RenderTargetBitmap _Bitmap = default(RenderTargetBitmap);
        public RenderTargetBitmap Bitmap { get { return _Bitmap; } set { SetProperty(ref _Bitmap, value); } }

        DateTime _Date = default(DateTime);
        public DateTime Date { get { return _Date; } set { SetProperty(ref _Date, value); } }
    }
}

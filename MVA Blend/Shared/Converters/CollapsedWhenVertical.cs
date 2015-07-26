using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Shared.Converters
{
    public class CollapsedWhenVertical: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var o = (Shared.Common.BaseViewModel.Orientations)value;
            switch (o)
            {
                case Shared.Common.BaseViewModel.Orientations.Horizontal:
                    return Visibility.Visible;
                case Shared.Common.BaseViewModel.Orientations.Vertical:
                    return Visibility.Collapsed;
                case Shared.Common.BaseViewModel.Orientations.Snap:
                    return Visibility.Collapsed;
                default:
                    throw new NotSupportedException(o.ToString());
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

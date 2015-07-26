using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Shared.Converters
{
    public class ReverseOrientationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var o = (Shared.Common.BaseViewModel.Orientations)value;
            switch (o)
            {
                case Shared.Common.BaseViewModel.Orientations.Horizontal:
                    return Windows.UI.Xaml.Controls.Orientation.Vertical;
                case Shared.Common.BaseViewModel.Orientations.Vertical:
                    return Windows.UI.Xaml.Controls.Orientation.Horizontal;
                case Shared.Common.BaseViewModel.Orientations.Snap:
                    return Windows.UI.Xaml.Controls.Orientation.Horizontal;
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

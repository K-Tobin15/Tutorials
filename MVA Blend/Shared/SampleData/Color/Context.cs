using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace Shared.SampleData.Color
{
    public static class Context
    {
        public static IEnumerable<ColorInfo> Colors()
        {
            return typeof(Windows.UI.Colors).GetRuntimeProperties()
                .Select(x => new ColorInfo { Color = (Windows.UI.Color)x.GetValue(null), Name = x.Name })
                .Where(x => x.Color != Windows.UI.Colors.Transparent)
                .OrderBy(x => x.Group)
                .ThenBy(x => new HSLColor(x.Color).Lightness)
                .ThenBy(x => new HSLColor(x.Color).Saturation);
        }

        public static IEnumerable<ColorGroup> Groups()
        {
            return Colors()
                .GroupBy(x => x.Group)
                .Select(x => new ColorGroup { Name = x.Key, Colors = x.ToArray() });
        }
    }
}

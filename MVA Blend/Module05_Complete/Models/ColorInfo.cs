using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Module05_Complete.Models
{
    public class ColorInfo
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public SolidColorBrush Brush { get { return new SolidColorBrush(this.Color); } }
    }
}

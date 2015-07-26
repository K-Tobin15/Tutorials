using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Shared.SampleData.Color
{
    public class ColorInfo
    {
        public Windows.UI.Color Color { get; set; }
        public string Name { get; set; }
        public SolidColorBrush Brush { get { return new SolidColorBrush { Color = this.Color }; } }
        public string Group
        {
            get
            {
                var c = new HSLColor(this.Color);
                var hue = c.Hue;
                var sat = c.Saturation;
                var lgt = c.Lightness;
                if (lgt < 0.2) return "Blacks";
                if (lgt > 0.8) return "Whites";
                if (sat < 0.25) return "Grays";
                if (hue < 30) return "Reds";
                if (hue < 90) return "Yellows";
                if (hue < 150) return "Greens";
                if (hue < 210) return "Cyans";
                if (hue < 270) return "Blues";
                if (hue < 330) return "Magentas";
                return "Reds";
            }
        }

        Shared.Microsoft.RelayCommand<ColorInfo> _SetSelectedCommand = null;
        public Shared.Microsoft.RelayCommand<ColorInfo> SetSelectedCommand
        {
            get
            {
                if (_SetSelectedCommand != null)
                    return _SetSelectedCommand;
                _SetSelectedCommand = new Shared.Microsoft.RelayCommand<ColorInfo>
                (
                    o => Shared.Services.MessageBus.Instance.Send("COLORSELECTED", this),
                    o => true
                );
                return _SetSelectedCommand;
            }
        }
    }
}

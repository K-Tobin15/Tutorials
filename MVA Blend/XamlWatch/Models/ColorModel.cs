using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace XamlWatch.Models
{
    public class ColorModel : BaseModel
    {
        public ColorModel(Color color, int colspan = 1, int rowspan = 1)
        {
            this.Color = color;
            this.ColSpan = colspan;
            this.RowSpan = rowspan;
        }
        Color _Color = default(Color);
        public Color Color { get { return _Color; } set { SetProperty(ref _Color, value); } }

        int _ColSpan = default(int);
        public int ColSpan { get { return _ColSpan; } set { SetProperty(ref _ColSpan, value); } }

        int _RowSpan = default(int);
        public int RowSpan { get { return _RowSpan; } set { SetProperty(ref _RowSpan, value); } }

        string _Name = default(string);
        public string Name { get { return _Name; } set { SetProperty(ref _Name, value); } }

    }
}

using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace XamlWatch.Models
{
    public class FaceModel : BaseModel
    {
        public FaceModel(string path)
        {
            this.Path = path;
        }

        string _Path = default(string);
        public string Path { get { return _Path; } set { SetProperty(ref _Path, value); } }
    }
}

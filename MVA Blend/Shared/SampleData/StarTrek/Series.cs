using Shared.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SampleData.StarTrek
{
    public class Series : BaseModel
    {
        string _Title = default(string);
        public string Title { get { return _Title; } set { SetProperty(ref _Title, value); } }

        string _Code = default(string);
        public string Code { get { return _Code; } set { SetProperty(ref _Code, value); } }

        ObservableCollection<Episode> _Episodes = new ObservableCollection<Episode>();
        public ObservableCollection<Episode> Episodes { get { return _Episodes; } }

        ObservableCollection<Character> _Characters = new ObservableCollection<Character>();
        public ObservableCollection<Character> Characters { get { return _Characters; } }
    }
}

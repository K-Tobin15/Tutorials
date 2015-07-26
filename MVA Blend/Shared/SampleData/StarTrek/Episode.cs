using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SampleData.StarTrek
{
    public class Episode: BaseModel
    {
        string _Title = default(string);
        public string Title { get { return _Title; } set { SetProperty(ref _Title, value); } }

        string _Stardate = default(string);
        public string Stardate { get { return _Stardate; } set { SetProperty(ref _Stardate, value); } }

        string _Description = default(string);
        public string Description { get { return _Description; } set { SetProperty(ref _Description, value); } }
    }
}

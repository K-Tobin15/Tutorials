using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlSpace.Models
{
    public class Character : BaseModel
    {
        string _Name = default(string);
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value);
                this.Photo = string.Format("ms-appx:///Images/{0}.jpg", value);
            }
        }

        string _Actor = default(string);
        public string Actor { get { return _Actor; } set { SetProperty(ref _Actor, value); } }

        string _Series = default(string);
        public string Series { get { return _Series; } set { SetProperty(ref _Series, value); } }

        string _Photo = default(string);
        public string Photo { get { return _Photo; } set { SetProperty(ref _Photo, value); } }
    }
}

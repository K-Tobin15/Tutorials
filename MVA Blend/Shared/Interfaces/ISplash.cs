using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace Shared.Interfaces
{
    public interface ISplash
    {
        SplashScreen SplashScreen { get; set; }
        Action Navigate { get; set; }
        void Start();
    }
}

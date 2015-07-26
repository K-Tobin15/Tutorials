using Shared.Common;
using Shared.Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common
{
    public abstract class BaseModel: BindableBase
    {
        int _Index = default(int);
        public int Index { get { return _Index; } set { SetProperty(ref _Index, value); } }

        int _ColSpan = default(int);
        public int ColSpan { get { return _ColSpan; } set { SetProperty(ref _ColSpan, value); } }

        int _RowSpan = default(int);
        public int RowSpan { get { return _RowSpan; } set { SetProperty(ref _RowSpan, value); } }
    }
}

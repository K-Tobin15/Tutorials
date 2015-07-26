using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class MessageBus
    {
        private MessageBus() { }
        private static MessageBus _Instance;
        public static MessageBus Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new MessageBus();
                return _Instance;
            }
        }

        private Dictionary<Type, List<Action<string, object>>> _Registrants
            = new Dictionary<Type, List<Action<string, object>>>();
        public void Register<T>(Action<string, object> action) where T : class
        {
            List<Action<string, object>> list;
            if (!_Registrants.ContainsKey(typeof(T)))
                _Registrants[typeof(T)] = new List<Action<string, object>>();
            list = _Registrants[typeof(T)];
            list.Add(action);
        }

        public void Unregister<T>() where T : class
        {
            if (_Registrants.ContainsKey(typeof(T)))
                _Registrants.Remove(typeof(T));
        }

        public void Send(string message, object payload = null)
        {
            foreach (var item in _Registrants.SelectMany(x => x.Value))
                item(message, payload);
        }
    }
}

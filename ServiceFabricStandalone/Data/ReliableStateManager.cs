using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Data
{
    public class ReliableStateManager : IReliableStateManager
    {
        private ConcurrentDictionary<string, object> _states = new ConcurrentDictionary<string, object>();

		public ITransaction CreateTransaction()
		{
			return new Transaction();
		}

        public Task SetStateAsync(string key, object value)
        {
            return Task.Run(() =>
            {
                _states[key] = value;
            //if (_states.ContainsKey(key))
            //    {
                    
            //    }else { 
            //    _states.Add(key, value);
            });
        }

		public T CreateInstance<T>() where T : class, IReliableState // IReliableDictionary<string, string>
		{
			//TODO: Hack. replace it with a factory and use dependecy injection for the different types
			if (typeof(T) == typeof(IReliableDictionary<string, string>))
			{
				var instance = new ReliableDictionary<string, string>();
				return instance as T;
			}

			return default(T);
		}

		public Task<T> GetOrAddAsync<T>(string name) where T : class, IReliableState
		{
			return Task.Run(() => {

				if (_states.ContainsKey(name))
				{
					return (T)_states[name];
				}
				else
				{
					var t = CreateInstance<T>();
					_states.GetOrAdd(name, t);
					return t;
				}
			});
		}
	}
}

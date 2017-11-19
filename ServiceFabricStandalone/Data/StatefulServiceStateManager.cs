using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Data
{
    internal class StatefulServiceStateManager : IReliableStateManager
    {
		ConcurrentDictionary<string, object> state;

		public ITransaction CreateTransaction()
		{
			return new Transaction();
		}

		//public Task<T> GetOrAddAsync<T>(string key) 
		//{
		//	return Task.Run(() => {
		//		var t = CreateInstance<T>();
		//		state.GetOrAdd(key, t);
		//		return t;
		//	});
		//}

		public T CreateInstance<T>() where T: class, IReliableState // IReliableDictionary<string, string>
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
				var t = CreateInstance<T>();
				state.GetOrAdd(name, t);
				return t;
			});
		}
	}
}

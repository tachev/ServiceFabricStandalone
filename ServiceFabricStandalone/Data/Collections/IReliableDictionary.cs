using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Data.Collections
{
	public interface IReliableDictionary<TKey, TValue> : IReliableCollection<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>, IEquatable<TKey>
	{
		Task<ConditionalValue<TValue>> TryGetValueAsync(ITransaction tx, TKey key);
		Task<TValue> AddOrUpdateAsync(ITransaction transaction, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory);
	}
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Data.Collections
{
	internal class ReliableDictionary<TKey, TValue> : IReliableDictionary<TKey, TValue>, ICommitable where TKey : IComparable<TKey>, IEquatable<TKey>
	{
		private ConcurrentDictionary<TKey, TValue> store = new ConcurrentDictionary<TKey, TValue>();
		public string Name { get; internal set; }

		public Task<TValue> AddOrUpdateAsync(ITransaction tx, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			return Task.Run(() =>
			{
				Transaction transaction = tx as Transaction;
				if (transaction.TransactionObject == null)
				{
					transaction.TransactionObject = new ConcurrentDictionary<TKey, TValue>();
				}

				transaction.Store = this;

				var store = transaction.TransactionObject as ConcurrentDictionary<TKey, TValue>;

				return store.AddOrUpdate(key, addValue, updateValueFactory);
			});
		}
		

		public Task<ConditionalValue<TValue>> TryGetValueAsync(ITransaction tx, TKey key)
		{
			return Task.Run(() =>
			{
				Transaction transaction = tx as Transaction;

				if (transaction.TransactionObject == null)
				{
					return GetConditionalValueFromStore(key, store);
				}

				var transactionStore = transaction.TransactionObject as ConcurrentDictionary<TKey, TValue>;
				ConditionalValue<TValue> result = GetConditionalValueFromStore(key, transactionStore);
				if (result == null)
				{
					return GetConditionalValueFromStore(key, store);
				}

				return result;
			});
		}

		private static ConditionalValue<TValue> GetConditionalValueFromStore(TKey key, ConcurrentDictionary<TKey, TValue> transactionStore)
		{
			return new ConditionalValue<TValue>
			{
				HasValue = transactionStore.TryGetValue(key, out TValue value),
				Value = value
			};
		}

		public Task CommitTransactionAsync(Transaction transaction)
		{
			return Task.Run(() =>
			{
				var transactionStore = transaction.TransactionObject as ConcurrentDictionary<TKey, TValue>;

				foreach (var item in transactionStore)
				{
					store.AddOrUpdate(item.Key, item.Value, (key, value) => value = item.Value);
				}
			});
		}
	}
}

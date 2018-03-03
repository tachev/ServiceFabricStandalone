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

		private object lockObject = new object();
		public string Name { get; internal set; }

		public Task<TValue> AddOrUpdateAsync(ITransaction tx, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			return Task.Run(() =>
			{
				Transaction transaction = tx as Transaction;
				if (transaction.TransactionObject == null)
				{
					transaction.TransactionObject = new TransactionContainer<TKey, TValue>();
				}

				transaction.Store = this;

				var transactionContainer = transaction.TransactionObject as TransactionContainer<TKey, TValue>;

				return transactionContainer.AddOrUpdate(key, addValue, updateValueFactory);
			});
		}

		public Task TryRemoveAsync(ITransaction tx, TKey key)
		{
			return Task.Run(() =>
			{
				Transaction transaction = tx as Transaction;
				if (transaction.TransactionObject == null)
				{
					transaction.TransactionObject = new TransactionContainer<TKey, TValue>();
				}

				transaction.Store = this;

				var transactionContainer = transaction.TransactionObject as TransactionContainer<TKey, TValue>;

				store.TryGetValue(key, out TValue value);

				return transactionContainer.Remove(key, value);
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
				if (transaction.TransactionObject == null)
				{
					return;
				}

				var transactionContainer = transaction.TransactionObject as TransactionContainer<TKey, TValue>;

				lock (lockObject)
				{
					foreach (var item in transactionContainer.ItemsToAddOrUpdate)
					{
						store.AddOrUpdate(item.Key, item.Value, (key, value) => value = item.Value);
					}
					foreach (var item in transactionContainer.ItemsToRemove)
					{
						store.TryRemove(item.Key, out TValue result);
					}
					transactionContainer.Clear();
				}
			});
		}

		public Task<bool> ContainsKeyAsync(ITransaction tx, TKey key)
		{
			return Task.Run(() =>
			{
				Transaction transaction = tx as Transaction;
				if (transaction.TransactionObject != null )
				{
					var transactionContainer = transaction.TransactionObject as TransactionContainer<TKey, TValue>;
					if (transactionContainer.ItemsToAddOrUpdate.ContainsKey(key)) {
						return true;
					}
					if (transactionContainer.ItemsToRemove.ContainsKey(key))
					{
						return false;
					}

					return store.ContainsKey(key);
				}

				return store.ContainsKey(key);
			});
		}
	}

	internal class TransactionContainer<TKey, TValue>
	{
		ConcurrentDictionary<TKey, TValue> _itemsToAddOrUpdate;
		public ConcurrentDictionary<TKey, TValue> ItemsToAddOrUpdate
		{
			get
			{
				if (_itemsToAddOrUpdate == null)
				{
					_itemsToAddOrUpdate = new ConcurrentDictionary<TKey, TValue>();
				}
				return _itemsToAddOrUpdate;
			}
		}

		ConcurrentDictionary<TKey, TValue> _itemsToRemove;
		public ConcurrentDictionary<TKey, TValue> ItemsToRemove
		{
			get
			{
				if (_itemsToRemove == null)
				{
					_itemsToRemove = new ConcurrentDictionary<TKey, TValue>();
				}
				return _itemsToRemove;
			}
		}

		internal TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (_itemsToRemove != null)
			{
				_itemsToRemove.TryRemove(key, out TValue result);
			}
			return ItemsToAddOrUpdate.AddOrUpdate(key, addValue, updateValueFactory);
		}

		internal bool Remove(TKey key, TValue value)
		{
			if (_itemsToAddOrUpdate != null)
			{
				_itemsToAddOrUpdate.TryRemove(key, out value);
			}
			if (value == null)
			{
				return false;
			}
			ItemsToRemove.AddOrUpdate(key, value, (k,v) => value);

			return true;
		}

		internal void Clear()
		{
			_itemsToRemove = null;
			_itemsToAddOrUpdate = null;
		}
	}
}

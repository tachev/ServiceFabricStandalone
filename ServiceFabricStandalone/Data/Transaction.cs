using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;

namespace Microsoft.ServiceFabric.Data
{
	public class Transaction : ITransaction
	{
		public Transaction()
		{
		}

		public async Task CommitAsync()
		{
			//TODO: Make this more generic
			var store = Store as ReliableDictionary<string, string>;
			if (store != null)
			{
				await store.CommitTransactionAsync(this);
			}
		}

		public void Dispose()
		{
		}

		internal object TransactionObject { get; set; }
		internal ICommitable Store { get; set; }
	}
}

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
			var store = Store as ICommitable;
			if (store != null)
			{
				await store.CommitTransactionAsync(this);
			}
		}

		public void Dispose()
		{
		}

		public void Abort()
		{
			Store = null;
			TransactionObject = null;
		}

		internal object TransactionObject { get; set; }
		internal ICommitable Store { get; set; }
	}
}

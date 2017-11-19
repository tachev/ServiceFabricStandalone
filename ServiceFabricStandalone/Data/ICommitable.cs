using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Data.Collections
{
    internal interface ICommitable
    {
		Task CommitTransactionAsync(Transaction tx);
	}
}

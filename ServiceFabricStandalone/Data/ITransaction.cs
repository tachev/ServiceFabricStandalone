using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Data
{
	public interface ITransaction : IDisposable
	{
		Task CommitAsync();
	}
}

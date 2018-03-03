using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Services.Runtime
{
	//TODO: Should this be IService?
	public abstract class StatefulServiceBase : IService
	{
		protected virtual async Task RunAsync(CancellationToken cancellationToken)
		{
			await Task.Run(() =>
			{

			});
		}
	}
}

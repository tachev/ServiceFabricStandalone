using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Services.Runtime
{
	public class StatelessService 
	{
        public StatelessService(StatelessServiceContext context)
        {

        }

        public StatelessServiceContext Context { get; }

        protected virtual IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return null;
        }

        protected virtual async Task RunAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {

            });
        }
    }
}

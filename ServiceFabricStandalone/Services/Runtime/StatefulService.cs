using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Services.Runtime
{
	public class StatefulService : StatefulServiceBase
	{
		//TODO: This hould not be static. It should have a way to resolve the state manager by the service
		private static IReliableStateManager _stateManager;

		public StatefulService(StatefulServiceContext context)
        {

        }

		public IReliableStateManager StateManager {
			get
			{
				if (_stateManager == null)
				{
					_stateManager = new ReliableStateManager();
				}
				return _stateManager;
			}
		}

		public StatefulServiceContext Context { get; }

		protected virtual IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
		{
			return null;
		}
		protected virtual IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
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

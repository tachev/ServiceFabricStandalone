using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Fabric;
using System.Threading.Tasks;
using UsersStateService.Interfaces;

namespace WebSite
{
	public class ServiceFabricUsersStateService : IUsersStateService
	{
		private Uri _serviceUri;
		private int userId;

#if STANDALONE
        public ServiceFabricUsersStateService()
        {
            _serviceUri = new Uri("fabric:/SharePlusEvo/UsersStateService");
        }
#else
		public ServiceFabricUsersStateService(StatelessServiceContext serviceContext)
		{
			_serviceUri = new Uri($"{serviceContext.CodePackageActivationContext.ApplicationName}/UsersStateService");
		}
#endif

		private IUsersStateService Proxy
		{
			get
			{
				userId = 0;//TODO: Add logic to get the currently logged user
				return ServiceProxy.Create<IUsersStateService>(_serviceUri, partitionKey:new Microsoft.ServiceFabric.Services.Client.ServicePartitionKey(userId));
			}
		}

		public Task<string> GetUserStateAsync()
		{
			return Proxy.GetUserStateAsync();
		}

		public Task SetUserStateAsync(string state)
		{
			return Proxy.SetUserStateAsync(state);
		}
	}
}

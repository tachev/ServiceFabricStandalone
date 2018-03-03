using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Services.Runtime
{
    public class ServiceRuntime
    {
		private static Dictionary<string, Func<StatelessServiceContext, IService>> serviceTypes = new Dictionary<string, Func<StatelessServiceContext, IService>>();
		private static Dictionary<string, Func<StatefulServiceContext, StatefulServiceBase>> statefulServicesTypes = new Dictionary<string, Func<StatefulServiceContext, StatefulServiceBase>>();

		//TODO: Check if that's the rigth logic for the running services
		private static Dictionary<string, IService> runningServices = new Dictionary<string, IService>();

		public static Task RegisterServiceAsync(string serviceTypeName, Func<StatelessServiceContext, IService> serviceFactory)
        {
            return Task.Run(() =>
            {
				if (!serviceTypes.ContainsKey(serviceTypeName))
				{
					serviceTypes.Add(serviceTypeName, serviceFactory);
				}
            });
        }

		public static Task RegisterServiceAsync(string serviceTypeName, Func<StatefulServiceContext, StatefulServiceBase> serviceFactory, TimeSpan? timeout = null, System.Threading.CancellationToken? cancellationToken = null)
		{
			return Task.Run(() =>
			{
				if (!statefulServicesTypes.ContainsKey(serviceTypeName))
				{
					statefulServicesTypes.Add(serviceTypeName, serviceFactory);
				}
			});
		}

		internal static IService CreateService(string serviceTypeName, StatefulServiceContext context)
		{
			//REVIEW: Should we do the same for the stateless services?
			if (runningServices.ContainsKey(serviceTypeName))
			{
				return runningServices[serviceTypeName];
			}
			else
			{
				var serviceFactory = statefulServicesTypes[serviceTypeName];
				var service = serviceFactory(context);

				runningServices.Add(serviceTypeName, service);

				return service;
			}
		}

		internal static IService CreateService(string serviceTypeName, StatelessServiceContext context)
        {
			var serviceFactory = serviceTypes[serviceTypeName];
			return serviceFactory(context);
        }
	}
}

using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Services.Runtime
{
    public class ServiceRuntime
    {
		private static Dictionary<string, Func<StatelessServiceContext, IService>> serviceTypes = new Dictionary<string, Func<StatelessServiceContext, IService>>();
		private static Dictionary<string, Func<StatefulServiceContext, StatefulServiceBase>> statefulServicesTypes = new Dictionary<string, Func<StatefulServiceContext, StatefulServiceBase>>();
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

		internal static StatefulServiceBase CreateService(string serviceTypeName, StatefulServiceContext context)
		{
			var serviceFactory = statefulServicesTypes[serviceTypeName];
			return serviceFactory(context);
		}

		internal static IService CreateService(string serviceTypeName, StatelessServiceContext context)
        {
            var serviceFactory = serviceTypes[serviceTypeName];
            return serviceFactory(context);
        }
	}
}

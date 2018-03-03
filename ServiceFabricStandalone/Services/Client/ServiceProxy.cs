using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ServiceFabric.Services.Client;
using System.Fabric;

namespace Microsoft.ServiceFabric.Services.Remoting.Client
{
    public class ServiceProxy
    {
        public static T Create<T>(Uri uri) where T:IService
        {
            //TODO: We should read this from ApplicationManifest.xml
            string serviceTypeName = uri.Segments.Last() + "Type";

            return (T)ServiceRuntime.CreateService(serviceTypeName, new StatelessServiceContext());
        }

		public static T Create<T>(Uri uri, ServicePartitionKey partitionKey) where T: IService
		{
			string serviceTypeName = uri.Segments.Last() + "Type";
			return (T)ServiceRuntime.CreateService(serviceTypeName, new StatefulServiceContext());
		}
	}
}

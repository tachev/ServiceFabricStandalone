using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ServiceFabric.Services.Client
{
    public class ServicePartitionKey
    {
		public ServicePartitionKey(long value)
		{
			Value = value;
		}

		public object Value { get; }
	}
}

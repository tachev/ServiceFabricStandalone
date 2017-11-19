using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ServiceFabric.Services.Communication.Runtime
{
    public class ServiceReplicaListener
    {
        private Func<object, object> p;

        public ServiceReplicaListener(Func<object, object> p)
        {
            this.p = p;
        }
    }
}

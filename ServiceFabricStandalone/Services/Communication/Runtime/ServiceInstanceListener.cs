using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ServiceFabric.Services.Communication.Runtime
{
    public class ServiceInstanceListener
    {
        private Func<object, object> p;

        public ServiceInstanceListener(Func<object, object> p)
        {
            this.p = p;
        }
    }
}

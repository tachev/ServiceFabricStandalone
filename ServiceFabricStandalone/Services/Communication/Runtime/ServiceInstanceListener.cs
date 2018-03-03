using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ServiceFabric.Services.Communication.Runtime
{
    public class ServiceInstanceListener
    {
        private Func<object, object> p;
        private string _name;

        public ServiceInstanceListener(Func<object, object> p)
        {
            this.p = p;
        }

        public ServiceInstanceListener(Func<object, object> p, string name)
        {
            this.p = p;
            this._name = name;
        }
    }
}

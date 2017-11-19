using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Text;

namespace Microsoft.ServiceFabric.Actors.Runtime
{
    public class ActorService : StatefulServiceBase
	{
        private ActorTypeInformation actorType;

        public ActorService(StatefulServiceContext context, ActorTypeInformation actorType)
        {
            this.Context = context;
            this.actorType = actorType;
        }

        public StatefulServiceContext Context { get; private set; }
    }
}

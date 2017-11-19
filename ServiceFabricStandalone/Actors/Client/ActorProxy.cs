using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System;

namespace Microsoft.ServiceFabric.Actors.Client
{
    public class ActorProxy
    {
        public static T Create<T>(ActorId actorId) where T:IActor
        {
            return ActorRuntime.CreateService<T>(null, null, actorId);
        }
        
    }
}
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Actors.Runtime
{
    public class ActorRuntime
    {

        //TODO:: very stupid implementation! :) It wouldnt work at all as the actor, but for now it will do the trick
        private static Dictionary<Type, Func<StatefulServiceContext, ActorTypeInformation, ActorService>> actors = new Dictionary<Type, Func<StatefulServiceContext, ActorTypeInformation, ActorService>>();
      
        internal static TActor CreateService<TActor>(StatefulServiceContext context, ActorTypeInformation actorTypeInformation, ActorId actorId) where TActor : IActor
        {
            //TODO: Implement
            var instance = Activator.CreateInstance(actors.Keys.First(), new object[] { null, actorId });

            return (TActor)instance;
            /*
            foreach (var actorType in actors.Keys)
            {
                if (actorType is TActor)
                {
                    var instance = Activator.CreateInstance(actorType);
                    return (TActor)instance;
                }
            }*/
            //var actorServiceFactory = actors[typeof(TActor)];
            //return default(TActor);// actorServiceFactory(context, actorTypeInformation);
        }

        public static Task RegisterActorAsync<TActor>(Func<StatefulServiceContext, ActorTypeInformation, ActorService> actorServiceFactory) where TActor : Actor
        {
            return Task.Run(() =>
            {
                actors.Add(typeof(TActor), actorServiceFactory);
            });
        }
    }
}

using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ServiceFabric.Actors
{
    public class Actor
    {
        private static Dictionary<string, ReliableStateManager> _stateManagers = new Dictionary<string, ReliableStateManager>();
        public Actor(ActorService actorService, ActorId actorId)
        {
            string actorIdString = actorId.ToString();
            if (_stateManagers.ContainsKey(actorIdString))
            {
                StateManager = _stateManagers[actorIdString];
            }
            else
            {
                StateManager = new ReliableStateManager();
                _stateManagers.Add(actorIdString, StateManager);
            }
        }

        public ReliableStateManager StateManager { get; private set; }
    }
}

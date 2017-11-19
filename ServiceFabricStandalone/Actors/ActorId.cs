using System;

namespace Microsoft.ServiceFabric.Actors
{
    public class ActorId
    {
        internal string actorId { get; private set; }

        public ActorId(Guid actorGuid)
        {
            this.actorId = actorGuid.ToString();
        }

        public ActorId(string actorId)
        {
            this.actorId = actorId;
        }

        public static ActorId CreateRandom()
        {
            return new ActorId(Guid.NewGuid());
        }
    }
}
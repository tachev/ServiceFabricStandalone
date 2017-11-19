using System;

namespace Microsoft.ServiceFabric.Actors.Runtime

{
    public class StatePersistenceAttribute : Attribute
    {
        public StatePersistenceAttribute(StatePersistence statePersistence)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Fabric
{
    public class ServiceContext
    {
        public Uri ServiceName { get; set; }
        public string ServiceTypeName { get; set; }
        public long InstanceId { get; set; }
        public Guid PartitionId { get; set; }
        public CodePackageActivationContext CodePackageActivationContext { get; set; }
        public NodeContext NodeContext { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ServiceFabric.Data
{
    public interface IReliableState
    {
		string Name { get; }
    }
}

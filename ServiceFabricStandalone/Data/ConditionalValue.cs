using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ServiceFabric.Data
{
	public class ConditionalValue<TValue>
	{
		public bool HasValue { get; set; }
		public TValue Value { get; set; }
	}
}

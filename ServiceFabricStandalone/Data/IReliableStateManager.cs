using System.Threading.Tasks;

namespace Microsoft.ServiceFabric.Data
{
	public interface IReliableStateManager
	{
		Task<T> GetOrAddAsync<T>(string name) where T : class, IReliableState;
		ITransaction CreateTransaction();
	}
}
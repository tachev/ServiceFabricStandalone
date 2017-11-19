using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Threading.Tasks;

namespace UsersStateService.Interfaces
{
    public interface IUsersStateService : IService
    {
		Task<string> GetUserStateAsync();

		Task SetUserStateAsync(string state);
	}
}

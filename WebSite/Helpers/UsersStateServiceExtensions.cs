using Microsoft.Extensions.DependencyInjection;
using System;
using UsersStateService.Interfaces;

namespace WebSite
{
    public static class UsersStateServiceExtensions
    {
        public static IServiceCollection AddUsersStateService(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            #if STANDALONE
            //Register standalone services
            Microsoft.ServiceFabric.Services.Runtime.ServiceRuntime.RegisterServiceAsync("UsersStateServiceType",
                    context => new UsersStateService.UsersStateService(context)).GetAwaiter().GetResult();
            #endif


            return services.AddSingleton<IUsersStateService, ServiceFabricUsersStateService>();
        }
    }
}

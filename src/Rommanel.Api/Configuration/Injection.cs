using Rommanel.Api.Configuration.Modules;

namespace Rommanel.Api.Configuration
{
    public static class Injection
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            InfrastructureModule.Load(services, configuration);
            RepositoryModule.Load(services);
            ServiceModule.Load(services);

            return services;
        }
    }
}

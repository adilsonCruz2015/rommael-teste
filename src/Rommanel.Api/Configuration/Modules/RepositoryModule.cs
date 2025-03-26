using Rommanel.Core.Interfaces;
using Rommanel.Infra.Repositories;

namespace Rommanel.Api.Configuration.Modules
{
    public static class RepositoryModule
    {
        public static void Load(IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}

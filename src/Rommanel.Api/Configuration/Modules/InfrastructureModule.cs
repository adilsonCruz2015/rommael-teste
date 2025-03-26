using Microsoft.EntityFrameworkCore;
using Rommanel.Application.Queries;
using Rommanel.Infra;
using System.Reflection;

namespace Rommanel.Api.Configuration.Modules
{
    public class InfrastructureModule
    {
        protected InfrastructureModule() { }

        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContextClass>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCustomerByFilterQuery).Assembly));
        }
    }
}

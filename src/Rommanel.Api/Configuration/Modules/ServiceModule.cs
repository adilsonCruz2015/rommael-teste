using Rommanel.Core.Interfaces;
using Rommanel.Core.ValueObject;

namespace Rommanel.Api.Configuration.Modules
{
    public static class ServiceModule
    {
        public static void Load(IServiceCollection services)
        {
            services.AddScoped<INotification, Notification>();
        }
    }
}

using Application.Interfaces.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.ServiceCollectionExtention
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection DatabaseServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseContext, IDatabaseContext>();

            return services;
        }
    }
}

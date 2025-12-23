using Application.Interfaces.Database;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseManagement.DatabaseConfiguration;

namespace Persistence.ServiceCollectionExtention
{
    //collectionExtention class to pack all database related services
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection DatabaseServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseContext, DatabaseContext>();

            return services;
        }
    }
}

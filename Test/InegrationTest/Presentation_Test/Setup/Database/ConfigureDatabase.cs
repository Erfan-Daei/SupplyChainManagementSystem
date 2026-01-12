using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseManagement.DatabaseConfiguration.AuditManager;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;

namespace Presentation_Test.Setup.Database
{
    public static class ConfigureDatabase
    {
        public static void ConfigureDatabaseForSqlLite(this IServiceCollection services)
        {
            //remove main DatabaseContext to use InMemory SqlLite
            var dbDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<DatabaseContext>));

            if (dbDescriptor != null)
                services.Remove(dbDescriptor);

            //user InMemory SqlLite connection
            var sqlLiteConnection = new SqliteConnection("DataSource=:memory:");
            sqlLiteConnection.Open();

            //add SqlLite service provier
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite(sqlLiteConnection));

            //register new DatabaseContext To IdatabaseContext interface
            services.AddScoped<IDatabaseContext>(sp =>
                sp.GetRequiredService<DatabaseContext>());

            //dont need actual log proccess for test
            services.AddScoped<IDatabaseContextAuditManager, FakeDatabaseContextAuditManager>();

            //create Database
            using var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            db.Database.EnsureDeletedAsync();
            db.Database.EnsureCreated();
        }
    }
}

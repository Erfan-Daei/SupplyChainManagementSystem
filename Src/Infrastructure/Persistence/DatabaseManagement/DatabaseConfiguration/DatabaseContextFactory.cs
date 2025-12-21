using Application.Interfaces.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence.DatabaseManagement.DatabaseConfiguration
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            // ساخت IConfiguration برای خواندن appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // مسیر پروژه WebApi
                .AddJsonFile("appsettings.json")
                .Build();

            // گرفتن ConnectionString
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DatabaseContext(optionsBuilder.Options, new DummyContainDBUser());
        }
    }

    // چون در DesignTime نمی‌تونی IContainDBUser واقعی تزریق کنی،
    // یک Dummy کلاس می‌سازی فقط برای Migration
    public class DummyContainDBUser : IDatabaseContext_UserInfo
    {
        public Guid UserId => Guid.Empty;
        public string UserFullName => "DesignTime";
        public Guid RoleId => Guid.Empty;
        public string RoleName => "DesignTime";
    }
}

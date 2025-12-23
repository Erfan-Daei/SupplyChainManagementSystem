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
            //use IConfiguration to get appsetting.json for sql connectionString
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //builder to instantiate databaseConnection for migartions
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DatabaseContext(optionsBuilder.Options, new DatabaseContext_UserInfo_Dummy());
        }
    }

    //dummy class for DatabaseContext constructor for proper migration process
    public class DatabaseContext_UserInfo_Dummy : IDatabaseContext_UserInfo
    {
        public Guid UserId => Guid.Empty;
        public string UserFullName => "DesignTime";
        public Guid RoleId => Guid.Empty;
        public string RoleName => "DesignTime";
    }
}

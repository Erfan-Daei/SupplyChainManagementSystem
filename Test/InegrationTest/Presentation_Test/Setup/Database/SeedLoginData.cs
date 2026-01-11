using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using Infrastructure.Hashing;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseManagement.DatabaseConfiguration;

namespace Presentation_Test.Setup.Database
{
    public class SeedLoginData
    {
        public DatabaseContext SetSeedLoginData(IServiceScope scope)
        {
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var password = "12345Ed@";
            var hashedPassword = new HashManagerService().BCryptHashPassword(password);
            var user = Domain.Entities.UserManagement.User.Create("Test", "Test@gmail.com", hashedPassword, SeedCompanies.DefaultCompanyId);
            db!.Users.Add(user);

            var userInRole = UserInRole.Create(user.UserId, SeedRoles.ViewerId);
            db.UserInRoles.Add(userInRole);

            db.SaveChanges();

            return db;
        }
    }
}

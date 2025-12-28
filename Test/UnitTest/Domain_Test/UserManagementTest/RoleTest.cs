using Domain.Entities.Common;
using Domain.Entities.UserManagement;

namespace Domain_Test.UserManagementTest
{
    public class RoleTest
    {
        [Fact]
        public void Create_Method_Should_Throw_Exception_For_Null_RoleName()
        {
            //arrange
            string roleName = string.Empty;

            //act & assert
            var result = Assert.Throws<ArgumentNullException>(() =>
            {
                var role = Role.Create(roleName);
            });

            Assert.Contains("نام نقش نمی تواند خالی باشد", result.Message);
        }

        [Fact]
        public void Create_Method_Should_Create_Role()
        {
            //arrange
            string roleName = SeedRoles.ViewerName;

            //act
            var role = Role.Create(roleName);

            //assert
            Assert.NotEqual(Guid.Empty, role.RoleId);
            Assert.Equal(roleName, role.RoleName);
            Assert.True(DateTime.UtcNow >= role.CreatedAt);
        }
    }
}

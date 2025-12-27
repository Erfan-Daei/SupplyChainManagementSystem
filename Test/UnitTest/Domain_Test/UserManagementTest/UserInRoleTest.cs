using Domain.Entities.UserManagement;

namespace Domain_Test.UserManagementTest
{
    public class UserInRoleTest
    {
        [Fact]
        public void CreateUserInRole_Method_Works_Correctly()
        {
            //arrange
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();

            //act
            var userInRole = UserInRole.CreateUserInRole(userId, roleId);

            //assert
            Assert.NotNull(userInRole);
            Assert.Equal(userId, userInRole.UserId);
            Assert.Equal(roleId, userInRole.RoleId);
            Assert.True(DateTime.UtcNow >= userInRole.CreatedAt);
        }
    }
}

using Domain.Entities.UserManagement;

namespace Domain_Test.UserManagementTest
{
    public class UserInRoleTest
    {
        [Fact]
        public void Create_Method_Should_Throw_Exception_For_Null_Values()
        {
            //arrange
            Guid userId = Guid.Empty;
            Guid roleId = Guid.Empty;

            //act & assert
            var result = Assert.Throws<ArgumentNullException>(() =>
            {
                var userInRole = UserInRole.Create(userId, roleId);
            });

            Assert.Contains("مقادیر UserId و RoleId نمیتوانند خالی باشند", result.Message);
        }

        [Fact]
        public void Create_Method_Should_Create_UserInRole()
        {
            //arrange
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();

            //act
            var userInRole = UserInRole.Create(userId, roleId);

            //assert
            Assert.NotNull(userInRole);
            Assert.Equal(userId, userInRole.UserId);
            Assert.Equal(roleId, userInRole.RoleId);
            Assert.True(DateTime.UtcNow >= userInRole.CreatedAt);
        }
    }
}

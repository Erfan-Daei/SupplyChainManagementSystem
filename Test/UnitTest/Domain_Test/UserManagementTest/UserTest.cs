using Domain.Entities.ServiceManagement;
using Domain.Entities.UserManagement;

namespace Domain_Test.UserManagementTest
{
    public class Usertest
    {
        [Fact]
        public void SetUserEmailConfirmed_Method_Wrok_Correctly()   //check SetUserEmailConfirmed method works properly
        {
            //arrange
            var _User = new User();
            var RandomBool = new Random().Next(2) == 0;   //randomly set is active true false  
            _User.UserEmailConfirmed = RandomBool;

            //act
            _User.SetUserEmailConfirmed();

            //assert
            Assert.True(_User.UserEmailConfirmed != RandomBool);
            Assert.True(_User.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void Check_CreateUser_Method_Wroks_Correctly()
        {
            //arrange
            var company = new Company() { CompanyId = Guid.NewGuid()};
            string userFullName = "Test";
            string userEmail = "Test@gmail.com;";
            string userPassword = "HashedPassword";

            //act
            var user = User.CreateUser(userFullName, userEmail, userPassword, company.CompanyId);

            //assert
            Assert.NotNull(user);
            Assert.Equal(user.UserFullName, userFullName);
            Assert.Equal(user.UserEmail, userEmail);
            Assert.Equal(user.UserPassword, userPassword);
            Assert.Equal(user.UserCompanyId, company.CompanyId);
            Assert.NotEqual(user.UserId, Guid.Empty);
            Assert.True(DateTime.UtcNow >= user.CreatedAt);
        }

        [Fact]
        public void SetUserInRole_Method_Wroks_Correctly()
        {
            //arrange
            var user = new User() { UserId = Guid.NewGuid()};
            var userInRole = new UserInRole() { UserId = user.UserId};

            //act
            user.SetUserInRole(userInRole);

            //assert
            Assert.Equal(user.UserInRoles, userInRole);
            Assert.True(DateTime.UtcNow >= user.UpdatedAt);
        }
    }
}

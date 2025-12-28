using Domain.Entities.ServiceManagement;
using Domain.Entities.UserManagement;

namespace Domain_Test.UserManagementTest
{
    public class Usertest
    {
        [Fact]
        public void ChangeUserEmailConfirmedState_Method_Wrok_Correctly()   //check SetUserEmailConfirmed method works properly
        {
            //arrange
            var user = new User();
            var randomBool = new Random().Next(2) == 0;   //randomly set is active true false  
            user.UserEmailConfirmed = randomBool;

            //act
            user.ChangeUserEmailConfirmedState();

            //assert
            Assert.True(user.UserEmailConfirmed != randomBool);
            Assert.True(user.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void SetUserInRole_Works_Correctly()
        {
            //arrange
            var user = new User() { UserId = Guid.NewGuid()};
            var userInRole = new UserInRole() { UserId = user.UserId};

            //act
            user.SetUserInRole(userInRole);

            //assert
            Assert.Equal(user.UserInRole, userInRole);
            Assert.True(DateTime.UtcNow >= user.UpdatedAt);
        }

        [Fact]
        public void Create_Method_Should_Throw_Exception_For_Null_Values()
        {
            //arrange
            string userFullName = string.Empty;
            string userEmail = string.Empty;
            string userPassword = string.Empty;

            //act & assert
            var result = Assert.Throws<ArgumentNullException>(() =>
            {
                var user = User.Create(userFullName, userEmail, userPassword, Guid.NewGuid());
            });

            Assert.Contains("تمامی مقادیر را پر کنید", result.Message);
        }

        [Fact]
        public void Check_CreateUser_Method_Create_User()
        {
            //arrange
            var company = new Company() { CompanyId = Guid.NewGuid()};
            string userFullName = "Test";
            string userEmail = "Test@gmail.com;";
            string userPassword = "HashedPassword";

            //act
            var user = User.Create(userFullName, userEmail, userPassword, company.CompanyId);

            //assert
            Assert.NotEqual(Guid.Empty, user.UserId);
            Assert.Equal(userFullName, user.UserFullName);
            Assert.Equal(userEmail, user.UserEmail);
            Assert.Equal(userPassword, user.UserPassword);
            Assert.Equal(user.UserCompanyId, company.CompanyId);
            Assert.True(DateTime.UtcNow >= user.CreatedAt);
        }
    }
}

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
    }
}

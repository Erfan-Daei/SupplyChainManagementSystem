using Domain.Entities.UserManagement;

namespace Domain_Test.UserManagementTest
{
    public class UserTokenTest
    {
        [Fact]
        public void SetIsExpired_Method_Work_Correctly()   //check SetIsExpire method works properly
        {
            //arrange
            UserToken userToken = new UserToken();

            //act
            userToken.SetIsExpired();

            //assert
            Assert.True(userToken.UserTokenIsExpired);
            Assert.True(userToken.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void CheckIsExpired_Method_Gives_True()   //check IsExpired method works properly
        {
            //arrange
            UserToken userToken = new UserToken();

            //act
            userToken.UserTokenExpireTime = new DateTime(2025, 01, 01, 20, 00, 00);
            userToken.CheckIsExpired();

            //assert
            Assert.True(userToken.UserTokenIsExpired);
            Assert.True(userToken.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void CheckIsExpired_Method_Gives_false()   //check IsExpired method works properly
        {
            //arrange
            UserToken userToken = new UserToken();

            //act
            userToken.UserTokenExpireTime = DateTime.UtcNow.AddMinutes(10);
            userToken.CheckIsExpired();

            //assert
            Assert.False(userToken.UserTokenIsExpired);
            Assert.True(userToken.UpdatedAt == null);
        }

        [Fact]
        public void SetIsUsed_Method_Work_Correctly()   //check SetIsUsed method work properly
        {
            //arrange
            UserToken userToken = new UserToken();

            //act
            userToken.SetIsUsed();

            //assert
            Assert.True(userToken.UserTokenIsExpired);
            Assert.True(userToken.UserTokenIsUsed);
            Assert.True(userToken.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void Default_ExpireTime_Should_Be_10_Minutes_From_Now()   //check ExpireTime default value
        {
            //arrange
            var userToken = new UserToken();

            //assert
            Assert.True(userToken.UserTokenExpireTime <= DateTime.UtcNow.AddMinutes(10));
        }

        [Fact]
        public void Create_Method_Should_Throw_Exception_For_Null_Values()
        {
            //arrange
            string userTokenValue = string.Empty;
            string userTokenType = string.Empty;
            Guid userId = Guid.Empty;

            //act & assert
            var result = Assert.Throws<ArgumentNullException>(() =>
            {
                var userToken = UserToken.Create(userTokenValue, userTokenType, 0, userId);
            });

            Assert.Contains("لطفا تمامی مقادیر را پر کنید", result.Message);
        }

        [Fact]
        public void CreateUserToken_Method_Works_Correctly()
        {
            //arrange
            string userTokenValue = "TokenValue";
            string userTokenType = "Type";
            int userTokenExpireMinutes = 10;
            var userId = Guid.NewGuid();

            //act
            var userToken = UserToken.Create(userTokenValue, userTokenType, userTokenExpireMinutes, userId);
            //assert
            Assert.NotEqual(userToken.UserTokenId, Guid.Empty);
            Assert.Equal(userTokenValue, userToken.UserTokenValue);
            Assert.Equal(userTokenType, userToken.UserTokenType);
            Assert.False(userToken.UserTokenIsExpired);
            Assert.False(userToken.UserTokenIsUsed);
            Assert.True(DateTime.UtcNow.AddMinutes(userTokenExpireMinutes) >= userToken.UserTokenExpireTime);
        }
    }
}

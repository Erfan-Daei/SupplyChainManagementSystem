using Domain.Entities.UserManagement;

namespace Domain_Test.UserManagementTest
{
    public class UserTokenTest
    {
        [Fact]
        public void SetIsExpire_Method_Work_Correctly()   //check SetIsExpire method works properly
        {
            //arrange
            UserToken userToken = new UserToken();

            //act
            userToken.SetIsExpire();

            //assert
            Assert.True(userToken.UserTokenIsExpired);
            Assert.True(userToken.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void IsExpired_Method_Token_Expired()   //check IsExpired method works properly
        {
            //arrange
            UserToken userToken = new UserToken();

            //act
            userToken.UserTokenExpireTime = new DateTime(2025, 01, 01, 20, 00, 00);
            userToken.IsExpired();

            //assert
            Assert.True(userToken.UserTokenIsExpired);
            Assert.True(userToken.UpdatedAt <= DateTime.UtcNow);
        }

        [Fact]
        public void IsExpired_Method_Token_Not_Expired()   //check IsExpired method works properly
        {
            //arrange
            UserToken userToken = new UserToken();

            //act
            userToken.UserTokenExpireTime = DateTime.UtcNow.AddMinutes(10);
            userToken.IsExpired();

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
        public void CreateUserToken_Method_Works_Correctly()
        {
            //arrange
            string userTokenValue = "TokenValue";
            string userTokenType = "Type";
            int userTokenExpireMinutes = 10;
            var userId = Guid.NewGuid();

            //act
            var userToken = UserToken.CreateUserToken(userTokenValue, userTokenType, userTokenExpireMinutes, userId);
            //assert
            Assert.NotNull(userToken);
            Assert.Equal(userToken.UserTokenValue, userTokenValue);
            Assert.Equal(userToken.UserTokenType, userTokenType);
            Assert.False(userToken.UserTokenIsExpired);
            Assert.False(userToken.UserTokenIsUsed);
            Assert.NotEqual(userToken.UserTokenId, Guid.Empty);
            Assert.True(DateTime.UtcNow.AddMinutes(userTokenExpireMinutes) >= userToken.UserTokenExpireTime);
        }
    }
}

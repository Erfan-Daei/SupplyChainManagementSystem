using Domain.Entities.Common;

namespace Domain.Entities.UserManagement
{
    public class UserToken : BaseEntity   //tbale to save User generated Tokens
    {
        public Guid UserTokenId { get; set; } = Guid.NewGuid();
        public string UserTokenValue { get; set; } = null!;
        public string UserTokenType { get; set; } = null!;
        public DateTime UserTokenExpireTime { get; set; } = DateTime.UtcNow.AddMinutes(10);
        public bool UserTokenIsExpired { get; set; } = false;
        public bool UserTokenIsUsed { get; set; } = false;

        //1 User to many UserToken relation
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }

        public void SetIsExpired()   //method to make UserToken Expire
        {
            UserTokenIsExpired = true;
            SetUpdatedAt();
        }

        public bool CheckIsExpired()   //method to check if UserToken is Expired
        {
            if (DateTime.UtcNow > UserTokenExpireTime)
            {
                SetIsExpired();
                return true;
            }
            return false;
        }

        public void SetIsUsed()   //method to make UserToken IsUsed
        {
            UserTokenIsUsed = true;
            SetIsExpired();
        }

        //creator method
        public static UserToken Create(string userTokenValue, string userTokenType, int userTokenExpireTime, Guid userId)
        {
            if (string.IsNullOrEmpty(userTokenValue) || string.IsNullOrEmpty(userTokenType) || userTokenExpireTime == 0 || userId == Guid.Empty)
                throw new ArgumentNullException("لطفا تمامی مقادیر را پر کنید");

            DateTime expireTime = DateTime.UtcNow.AddMinutes(10);

            if (userTokenType == Domain.Entities.Common.UserTokenType.RefreshToken.ToString())
                expireTime = DateTime.UtcNow.AddDays(userTokenExpireTime);

            else if (userTokenType == Domain.Entities.Common.UserTokenType.EmailConfirmation.ToString())
                expireTime = DateTime.UtcNow.AddMinutes(userTokenExpireTime);

            return new UserToken
            {
                UserTokenId = Guid.NewGuid(),
                UserTokenValue = userTokenValue,
                UserTokenType = userTokenType,
                UserTokenExpireTime = expireTime,
                UserTokenIsExpired = false,
                CreatedAt = DateTime.UtcNow,
                UserTokenIsUsed = false,
                UserId = userId
            };
        }
    }
}

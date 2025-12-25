using Domain.Entities.Common;

namespace Domain.Entities.UserManagement
{
    public class UserToken : BaseEntity   //tbale to save User generated Tokens
    {
        public Guid UserTokenId { get; set; }
        public string UserTokenValue { get; set; }
        public string UserTokenType { get; set; }
        public DateTime UserTokenExpireTime { get; set; } = DateTime.UtcNow.AddMinutes(10);
        public bool UserTokenIsExpired { get; set; } = false;

        public void SetIsExpire()   //method to make UserToken Expire
        {
            UserTokenIsExpired = true;
            SetUpdatedAt();
        }
        public bool IsExpired()   //method to check if UserToken is Expired
        {
            if (DateTime.UtcNow > UserTokenExpireTime)
            {
                SetIsExpire();
                return true;
            }
            return false;
        }

        public bool UserTokenIsUsed { get; set; } = false;

        public void SetIsUsed()   //method to make UserToken IsUsed
        {
            UserTokenIsUsed = true;
            SetIsExpire();
        }

        //1 User to many UserToken relation
        public User User { get; set; }
        public Guid UserId { get; set; }

    }
}

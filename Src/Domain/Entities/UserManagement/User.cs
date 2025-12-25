using Domain.Entities.Common;
using Domain.Entities.ServiceManagement;

namespace Domain.Entities.UserManagement
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public bool UserEmailConfirmed { get; set; } = false;
        public void SetUserEmailConfirmed()   //method for automated userEmailConfirmation update
        {
            UserEmailConfirmed = !UserEmailConfirmed;
            SetUpdatedAt();
        }
        public string UserPassword { get; set; }

        public Company UserCompany { get; set; }
        public Guid UserCompanyId { get; set; }

        public UserInRole UserInRoles { get; set; }   // 1 user to 1 userInRole
        public List<UserToken> UserTokens { get; set; } = new List<UserToken>();  //1 User to many UserTokens
    }
}

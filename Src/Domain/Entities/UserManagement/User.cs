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
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }

        public UserInRole UserInRoles { get; set; }   // 1 user to 1 userInRole
    }
}

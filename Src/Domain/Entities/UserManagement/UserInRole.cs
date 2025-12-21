using Domain.Entities.Common;

namespace Domain.Entities.UserManagement
{
    public class UserInRole : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }

        public Role Role { get; set; }
        public Guid RoleId { get; set; }
    }
}

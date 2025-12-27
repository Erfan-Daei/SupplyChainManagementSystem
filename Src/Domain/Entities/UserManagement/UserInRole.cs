using Domain.Entities.Common;

namespace Domain.Entities.UserManagement
{
    public class UserInRole : BaseEntity   //table to save user and role relations
    {
        public User User { get; set; }
        public Guid UserId { get; set; }

        public Role Role { get; set; }
        public Guid RoleId { get; set; }

        //creator method
        public static UserInRole CreateUserInRole(Guid userId, Guid roleId)
        {
            return new UserInRole
            {
                UserId = userId,
                RoleId = roleId,
                CreatedAt = DateTime.UtcNow
            }; 
        }
    }
}

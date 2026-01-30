using Domain.Entities.Common;

namespace Domain.Entities.UserManagement
{
    public class UserInRole : BaseEntity   //table to save user and role relations
    {
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }

        public Role Role { get; set; } = null!;
        public Guid RoleId { get; set; }

        //creator method
        public static UserInRole Create(Guid userId, Guid roleId)
        {
            if (userId == Guid.Empty || roleId == Guid.Empty)
                throw new ArgumentNullException("مقادیر UserId و RoleId نمیتوانند خالی باشند");

            return new UserInRole
            {
                UserId = userId,
                RoleId = roleId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}

using Domain.Entities.Common;

namespace Domain.Entities.UserManagement
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; } = Guid.NewGuid();
        public string RoleName { get; set; } = null!;

        // 1 role to many userInRoles
        public ICollection<UserInRole> UserInRoles { get; set; } = [];

        //creator Method
        public static Role Create(string roleName)
        {
            if(string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("نام نقش نمی تواند خالی باشد");

            return new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = roleName,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}

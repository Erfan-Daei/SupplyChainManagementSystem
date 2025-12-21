using Domain.Entities.Common;

namespace Domain.Entities.UserManagement
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; } = new List<UserInRole>();
    }
}

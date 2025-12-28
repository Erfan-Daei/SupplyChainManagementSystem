using Domain.Entities.Common;

namespace Domain.Entities.LogManagement
{
    public class Audit   //log table to save database changes
    {
        // all properties are private set to avoid edit and update funtions
        
        public Guid AuditId { get; private set; } = Guid.NewGuid();

        public Guid UserId { get; private set; }
        public string UserFullName { get; private set; } = null!;

        public Guid RoleId { get; private set; }
        public string RoleName { get; private set; } = null!;

        public string Action { get; private set; } = null!;   //which CRUD funtions happend
        public DateTime ActionAtTime { get; private set; } = DateTime.UtcNow;
        public string ActionOnEntity { get; private set; } = null!;   //on which Entity changes happend

        //constructor for automated log saver on database
        public Audit(Guid userId, string userFullName, Guid roleId, string roleName, string action, string actionOnEntity)
        {
            AuditId = Guid.NewGuid();
            UserId = userId;
            UserFullName = userFullName;
            RoleId = roleId == Guid.Empty ? SeedRoles.ViewerId : roleId;
            RoleName = roleName == string.Empty ? SeedRoles.ViewerName : roleName;
            Action = action;
            ActionOnEntity = actionOnEntity;
            ActionAtTime = DateTime.UtcNow;            
        }
    }
}

namespace Domain.Entities.LogManagement
{
    public class Audit
    {
        public Guid AuditId { get; private set; }

        public Guid UserId { get; private set; }
        public string UserFullName { get; private set; }

        public Guid RoleId { get; private set; }
        public string RoleName { get; private set; }

        public string Action { get; private set; }
        public DateTime ActionAtTime { get; private set; } = DateTime.Now;
        public string ActionOnEntity { get; private set; }

        public Audit(Guid userId, string userFullName, Guid roleId, string roleName, string action, string actionOnEntity)
        {
            AuditId = Guid.NewGuid();
            UserId = userId;
            UserFullName = userFullName;
            RoleId = roleId;
            RoleName = roleName;
            Action = action;
            ActionOnEntity = actionOnEntity;
        }
    }
}

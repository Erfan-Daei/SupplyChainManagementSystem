using Application.Interfaces.Database.DatabaseConfiguration;
using Common.AuditAction;
using Domain.Entities.LogManagement;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.DatabaseManagement.DatabaseConfiguration.AuditManager
{
    //class to make list of Audit for auto log proccess
    public class DatabaseContextAuditManager : IDatabaseContextAuditManager
    {
        private readonly IDatabaseContext_UserInfo _userInfo;   //inject UserInfo for Audit
        public DatabaseContextAuditManager(IDatabaseContext_UserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public IEnumerable<Audit> CreateAudits(IEnumerable<EntityEntry> entries)
        {
            List<Audit> audits = new List<Audit>();

            var @userId = _userInfo?.UserId ?? Guid.Empty;
            var @userFullName = _userInfo?.UserFullName ?? string.Empty;
            var @roleId = _userInfo?.RoleId ?? Guid.Empty;
            var @roleName = _userInfo?.RoleName ?? string.Empty;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Deleted && entry.Entity is not UserToken)
                    throw new InvalidOperationException("must soft delete, entities cannot be deleted.");

                ////make sure Audit cant be updated or deleted
                if (entry.Entity is Audit && (entry.State == EntityState.Modified))
                    throw new InvalidOperationException("Audit logs cannot be modified or deleted.");

                //check if User is new and hasnt LoggedIn
                if (entry.Entity is User newUser && entry.State == EntityState.Added && @userId == Guid.Empty)
                {
                    @userId = newUser.UserId;
                    @userFullName = newUser.UserFullName;
                    @roleId = newUser.UserInRole.RoleId;
                    @roleName = newUser.UserInRole.Role.RoleName;
                }

                //check if User is new and hasnt LoggedIn and added new UserInRole
                else if (entry.Entity is UserInRole newUserInRole && (entry.State == EntityState.Added) && @userId == Guid.Empty)
                {
                    @userId = newUserInRole.UserId;
                    @userFullName = newUserInRole.User.UserFullName;
                    @roleId = newUserInRole.RoleId;
                    @roleName = newUserInRole.Role.RoleName;
                }

                //check to not log for UserToken Entity
                else if (entry.Entity is UserToken)
                {
                    continue;
                }

                //check soft delete action for log in Audit
                var HasIsDeletedProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "IsDeleted");
                if (HasIsDeletedProp != null)
                {
                    if (entry.State == EntityState.Modified && Convert.ToBoolean(HasIsDeletedProp.CurrentValue) == true)
                    {
                        audits.Add(new Audit(
                            userId: @userId,
                            userFullName: @userFullName,
                            roleId: @roleId,
                            roleName: @roleName,
                            action: nameof(AuditAction.SoftDeleted),
                            actionOnEntity: entry.Entity.GetType().Name
                        ));
                        continue;
                    }
                }
                audits.Add(new Audit(
                    userId: @userId,
                    userFullName: @userFullName,
                    roleId: @roleId,
                    roleName: @roleName,
                    action: entry.State.ToString(),
                    actionOnEntity: entry.Entity.GetType().Name

                ));
            }

            return audits;
        }
    }
}

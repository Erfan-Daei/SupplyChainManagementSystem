using Application.Interfaces.Database.DatabaseConfiguration;
using Domain.Entities.LogManagement;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.DatabaseManagement.DatabaseConfiguration
{
    //class to make list of Audit for auto log proccess
    public class DatabaseContextAuditManager
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
                ////make sure Audit cant be updated or deleted
                if (entry.Entity is Audit && (entry.State == EntityState.Modified || entry.State == EntityState.Deleted))
                    throw new InvalidOperationException("Audit logs cannot be modified or deleted.");

                //check if User is new and hasnt LoggedIn
                if (entry.Entity is User newUser && entry.State == EntityState.Added && @userId == Guid.Empty)
                {
                    @userId = newUser.UserId;
                    @userFullName = newUser.UserFullName;
                    @roleId = newUser.UserInRoles.RoleId;
                    @roleName = newUser.UserInRoles.Role.RoleName;
                }

                //check if User is new and hasnt LoggedIn and added new UserInRole
                else if (entry.Entity is UserInRole newUserInRole && (entry.State == EntityState.Added) && @userId == Guid.Empty)
                {
                    @userId = newUserInRole.UserId;
                    @userFullName = newUserInRole.User.UserFullName;
                    @roleId = newUserInRole.RoleId;
                    @roleName = newUserInRole.Role.RoleName;
                }

                //check if User is new and hasnt LoggedIn and added new UserToken
                else if (entry.Entity is UserToken newUserToken && (entry.State == EntityState.Added) && @userId == Guid.Empty)
                {
                    @userId = newUserToken.UserId;
                    @userFullName = newUserToken.User.UserFullName;
                    @roleId = newUserToken.User?.UserInRoles?.RoleId?? Guid.Empty;
                    @roleName = newUserToken.User?.UserInRoles?.Role.RoleName?? string.Empty;
                }

                audits.Add(new Audit(
                    userId: @userId,
                    @userFullName: @userFullName,
                    @roleId: @roleId,
                    @roleName: @roleName,
                    entry.State.ToString(),
                    entry.Entity.GetType().Name
                ));
            }
            return audits;
        }
    }
}

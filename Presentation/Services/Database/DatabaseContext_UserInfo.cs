using Application.Interfaces.Database.DatabaseConfiguration;

namespace Presentation.Services.Database
{
    public class DatabaseContext_UserInfo : IDatabaseContext_UserInfo
    {
        public Guid UserId => Guid.Empty;
        public string UserFullName => string.Empty;
        public Guid RoleId => Guid.Empty;
        public string RoleName => string.Empty;

    }
}

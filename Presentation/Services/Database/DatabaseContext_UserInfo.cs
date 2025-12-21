using Application.Interfaces.Database;
using System.Security.Claims;

namespace Presentation.Services.Database
{
    public class DatabaseContext_UserInfo : IDatabaseContext_UserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DatabaseContext_UserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());
        public string UserFullName => _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;
        public Guid RoleId => Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("RoleId") ?? Guid.Empty.ToString());
        public string RoleName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

    }
}

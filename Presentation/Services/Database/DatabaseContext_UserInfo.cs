using Application.Interfaces.Database.DatabaseConfiguration;
using System.Security.Claims;

namespace Presentation.Services.Database
{
    public class DatabaseContext_UserInfo : IDatabaseContext_UserInfo
    {
        private readonly IHttpContextAccessor _context;
        public DatabaseContext_UserInfo(IHttpContextAccessor context)
        {
            _context = context;
        }
        public Guid UserId
        {
            get
            {
                var stringId = _context.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                return stringId != null ? Guid.Parse(stringId) : Guid.Empty;
            }
        }
        public string UserFullName => _context.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        public Guid RoleId
        {
            get
            {
                var stringId = _context.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value;
                return stringId != null ? Guid.Parse(stringId) : Guid.Empty;
            }
        }
        public string RoleName => _context.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty;

    }
}

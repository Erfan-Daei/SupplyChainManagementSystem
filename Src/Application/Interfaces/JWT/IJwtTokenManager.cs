using Domain.Entities.UserManagement;
using System.Security.Claims;

namespace Application.Interfaces.JWT
{
    //interface for Jwt management and mthods
    public interface IJwtTokenManager
    {
        string GenerateToken(User user, Role userRole);

        Guid GetUserId(IEnumerable<Claim> claims);

        string? GetUserRole(IEnumerable<Claim> claims);
    }
}

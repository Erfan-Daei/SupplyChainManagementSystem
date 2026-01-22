using Domain.Entities.UserManagement;
using System.Security.Claims;

namespace Application.Interfaces.JWT
{
    //interface for Jwt management and mthods
    public interface IJwtTokenManager
    {
        string GenerateToken(User user, Role userRole);   //generate JwtToken

        Guid GetUserId(IEnumerable<Claim> claims);   //get UserId from JwtToken

        string? GetUserRole(IEnumerable<Claim> claims);   //get UserRole from JwtToken
    }
}

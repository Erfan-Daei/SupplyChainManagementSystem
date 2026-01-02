using Domain.Entities.UserManagement;

namespace Application.Interfaces.JWT
{
    //interface for Jwt management and mthods
    public interface IJwtTokenManager
    {
        string GenerateToken(User user, Role userRole);
    }
}

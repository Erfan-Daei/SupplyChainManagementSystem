using Application.Interfaces.JWT;
using Domain.Entities.UserManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.JWT
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly JwtSettings _jwtSettings;   //POCO class to bind data from appsetings.json
        public JwtTokenManager(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(User user, Role userRole)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.UserEmail),
                new Claim(ClaimTypes.Version, user.UserLogOutVersion.ToString()),   //for LogOut
                new Claim(ClaimTypes.Role, userRole.RoleName),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                notBefore: DateTime.UtcNow,
                claims: claims,
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Guid GetUserId(IEnumerable<Claim> claims)
        {
            var userId = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(userId))
                return Guid.Empty;

            return Guid.Parse(userId);
        }

        public string? GetUserRole(IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))?.Value ?? string.Empty;
        }
    }
}

using System.Security.Claims;

namespace Application.Interfaces.Authorization
{
    public interface IAuthManager
    {
        Task<bool> CheckAccessToChangeRole(IEnumerable<Claim> adminClaims, Guid userId);
        Task<bool> ChechAccessToCompany(IEnumerable<Claim> adminClaims, Guid compnayId);
        Task<bool> ChechAccessToService(IEnumerable<Claim> adminClaims, Guid serviceId);
    }
}

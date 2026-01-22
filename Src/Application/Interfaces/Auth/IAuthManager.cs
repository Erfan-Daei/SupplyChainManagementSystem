using System.Security.Claims;

namespace Application.Interfaces.Auth
{
    public interface IAuthManager
    {
        Task<bool> CheckAccessToUser(IEnumerable<Claim> adminClaims, Guid userId);
        Task<bool> ChechAccessToCompany(IEnumerable<Claim> adminClaims, Guid compnayId);
        Task<bool> ChechAccessToService(IEnumerable<Claim> adminClaims, Guid serviceId);
        Task<bool> CheckAccessToAddSupplyRelation(IEnumerable<Claim> adminClaims, Guid supplierCompanyId, Guid consumerCompanyId);
        Task<bool> CheckAccessToSupplyRelation(IEnumerable<Claim> adminClaims, Guid supplyRelationId);
    }
}

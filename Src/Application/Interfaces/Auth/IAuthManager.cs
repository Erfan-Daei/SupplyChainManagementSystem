using System.Security.Claims;

namespace Application.Interfaces.Auth
{
    //Interface to implement methods to check access to entities and fields
    public interface IAuthManager
    {
        Task<bool> CheckAccessToUser(IEnumerable<Claim> adminClaims, Guid userId);   //Check Admin and User CompanyId
        Task<bool> CheckAccessToCompany(IEnumerable<Claim> adminClaims, Guid compnayId);   //Check Admin and Company CompanyId
        Task<bool> CheckAccessToService(IEnumerable<Claim> adminClaims, Guid serviceId);   //Check Admin and Service CreatorCompanyId
        Task<bool> CheckAccessToAddSupplyRelation(IEnumerable<Claim> adminClaims,Guid consumerCompanyId);   //Check Admin or ConsumerCompanyId
        Task<bool> CheckAccessToSupplyRelation(IEnumerable<Claim> adminClaims, Guid supplyRelationId);   //Check Admin and SupplyRelation SupplierCompanyId or ConsumerCompanyId
        Task<bool> CheckAccessToConfirmSupplyRelation(IEnumerable<Claim> adminClaims, Guid supplyRelationId);   //Check Admin or SupplyRelation SupplierCompanyId
    }
}

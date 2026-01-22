using Application.Interfaces.Auth;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Domain.Entities.Common;
using System.Security.Claims;

namespace Infrastructure.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserRepository_Query _user_Query;
        private readonly IServiceRepository_Query _service_Query;
        private readonly ISupplyRelationRepository_Query _supplyRelation_Query;
        public AuthManager(IUserRepository_Query user_Query
            , IServiceRepository_Query service_Query
            , ISupplyRelationRepository_Query supplyRelation_Query)
        {
            _user_Query = user_Query;
            _service_Query = service_Query;
            _supplyRelation_Query = supplyRelation_Query;
        }

        //check admin role which can access to user to change its role
        public async Task<bool> CheckAccessToUser(IEnumerable<Claim> adminClaims, Guid userId)
        {
            try
            {
                var AdminRole = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))!.Value;
                if (AdminRole.Equals(SeedRoles.AdminName))
                    return true;

                var adminId = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value;

                var adminCompany = await _user_Query.GetUserCompanyIdByUserIdAsync(Guid.Parse(adminId));

                var userCompany = await _user_Query.GetUserCompanyIdByUserIdAsync(userId);

                if (userCompany == Guid.Empty)
                    return false;

                if (adminCompany != userCompany)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckAccessToCompany(IEnumerable<Claim> adminClaims, Guid compnayId)
        {
            try
            {
                var AdminRole = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))!.Value;
                if (AdminRole.Equals(SeedRoles.AdminName))
                    return true;

                var adminId = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value;

                var adminCompany = await _user_Query.GetUserCompanyIdByUserIdAsync(Guid.Parse(adminId));

                if (adminCompany != compnayId)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckAccessToService(IEnumerable<Claim> adminClaims, Guid serviceId)
        {
            try
            {
                var AdminRole = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))!.Value;
                if (AdminRole.Equals(SeedRoles.AdminName))
                    return true;

                var adminId = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value;

                var adminCompany = await _user_Query.GetUserCompanyIdByUserIdAsync(Guid.Parse(adminId));

                var serviceSupplierCompanyId = await _service_Query.GetServiceCreatorCompanyIdByIdAsync(serviceId);

                if (adminCompany != serviceSupplierCompanyId)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckAccessToAddSupplyRelation(IEnumerable<Claim> adminClaims, Guid supplierCompanyId, Guid consumerCompanyId)
        {
            try
            {
                var AdminRole = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))!.Value;
                if (AdminRole.Equals(SeedRoles.AdminName))
                    return true;

                var adminId = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value;

                var adminCompany = await _user_Query.GetUserCompanyIdByUserIdAsync(Guid.Parse(adminId));


                if (adminCompany != supplierCompanyId && adminCompany != consumerCompanyId)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckAccessToSupplyRelation(IEnumerable<Claim> adminClaims, Guid supplyRelationId)
        {
            try
            {
                var AdminRole = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))!.Value;
                if (AdminRole.Equals(SeedRoles.AdminName))
                    return true;

                var adminId = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value;

                var adminCompany = await _user_Query.GetUserCompanyIdByUserIdAsync(Guid.Parse(adminId));

                var supplyRelation = await _supplyRelation_Query.GetSupplyRelationByIdAsync(supplyRelationId);
                if (supplyRelation == null)
                    return false;

                if (adminCompany != supplyRelation.SupplierCompanyId && adminCompany != supplyRelation.ConsumerCompanyId)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

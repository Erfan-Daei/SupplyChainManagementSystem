using Application.Interfaces.Authorization;
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
        public AuthManager(IUserRepository_Query user_Query
            , IServiceRepository_Query service_Query)
        {
            _user_Query = user_Query;
            _service_Query = service_Query;
        }

        //check admin role which can access to user to change its role
        public async Task<bool> CheckAccessToChangeRole(IEnumerable<Claim> adminClaims, Guid userId)
        {
            try
            {
                var AdminRole = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))!.Value;
                if (AdminRole.Equals(SeedRoles.AdminName))
                    return true;

                var adminId = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value;

                var adminCompany = await _user_Query.GetUserCompanyIdAsync(Guid.Parse(adminId));

                var userCompany = await _user_Query.GetUserCompanyIdAsync(userId);

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

        public async Task<bool> ChechAccessToCompany(IEnumerable<Claim> adminClaims, Guid compnayId)
        {
            try
            {
                var AdminRole = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))!.Value;
                if (AdminRole.Equals(SeedRoles.AdminName))
                    return true;

                var adminId = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value;

                var adminCompany = await _user_Query.GetUserCompanyIdAsync(Guid.Parse(adminId));

                if (adminCompany != compnayId)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ChechAccessToService(IEnumerable<Claim> adminClaims, Guid serviceId)
        {
            try
            {
                var AdminRole = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))!.Value;
                if (AdminRole.Equals(SeedRoles.AdminName))
                    return true;

                var adminId = adminClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value;

                var adminCompany = await _user_Query.GetUserCompanyIdAsync(Guid.Parse(adminId));

                var serviceSupplierCompanyId = await _service_Query.GetServiceSupplierCompanyIdById(serviceId);

                if (adminCompany != serviceSupplierCompanyId)
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

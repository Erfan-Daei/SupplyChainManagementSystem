using Domain.Entities.UserManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository
{
    //interface for all User Query services
    public interface IUserRepository_Query
    {
        Task<bool> CheckEmailExistAsync(string userEmail);   //check Emai even among SoftDeleted Users
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<UserToken?> GetEmailConfirmationTokenValueAsync(Guid userId);   //get UserToken which UserTokenType is EmailConfirmation
        Task<User?> GetUserByEmailAsync(string userEmail);
        Task<Role?> GetUserRoleByUserIdAsync(Guid userId);
        Task<UserToken?> GetRefreshTokenByUserIdAsync(Guid userId);   //get UserToken which UserTokenType is RefreshToken
        Task<Guid> GetUserCompanyIdByUserIdAsync(Guid userId);
        Task<User?> GetUserWithUserInRoleByUserIdAsync(Guid userId);
        Task<UserToken?> GetUserTokenByRefreshTokenAsync(string hashedRefreshToken);
        Task<List<User>?> GetAllUsersByCompanyId(Guid companyId);
        Task<User?> GetUserDetailByIdAsync(Guid userId);
        Task<UserToken?> GetUserChangePasswordTokenAsync(Guid userId);
        Task<UserToken?> GetUserTempPasswordAsync(Guid userId);
    }
}
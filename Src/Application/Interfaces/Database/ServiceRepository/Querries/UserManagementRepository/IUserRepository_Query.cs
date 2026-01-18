using Domain.Entities.UserManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository
{
    public interface IUserRepository_Query   //interface centeralize all User table Query (Select)  methods
    {
        Task<bool> CheckEmailExistAsync(string userEmail);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<UserToken?> GetEmailConfirmationTokenValueAsync(Guid userId);
        Task<User?> GetUserByEmailAsync(string userEmail);
        Task<Role?> GetUserRoleByUserIdAsync(Guid userId);
        Task<UserToken?> GetUserTokenByUserIdAsync(Guid userId);
        Task<Guid> GetUserCompanyIdAsync(Guid userId);
        Task<User?> GetUserWithUserInRoleByUserIdAsync(Guid userId);
        Task<UserToken?> GetUserTokenByRefreshTokenAsync(string hashedToken);
        Task<List<User>?> GetAllUsersByCompanyId(Guid companyId);
    }
}
using Domain.Entities.UserManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository
{
    public interface IUserRepository_Query   //interface centeralize all User table Query (Select)  methods
    {
        Task<bool> CheckEmailExistAsync(string userEmail);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<UserToken?> GetEmailConfirmationTokenValueAsync(Guid userId);
    }
}
using Domain.Entities.UserManagement;

namespace Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository
{
    public interface IUserRepository_Command   //interface centeralize all User table Command (Create, Update, Delete)  methods
    {
        Task CreateUserAsync(User user, UserInRole userInRole);
        Task AddUserTokenAsync(UserToken userToken);
        Task DeleteUserTokenAsync(UserToken userToken);
        Task AddUserLogOutVersion(Guid userId);
        Task SaveChangesAsync();
    }
}

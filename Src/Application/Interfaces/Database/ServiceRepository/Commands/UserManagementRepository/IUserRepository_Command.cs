using Common.Output;
using Domain.Entities.UserManagement;

namespace Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository
{
    public interface IUserRepository_Command   //interface centeralize all User table Command (Create, Update, Delete)  methods
    {
        Task<ResultDto> CreateUserAsync(User user, UserInRole userInRole);
        Task<ResultDto> AddUserTokenAsync(UserToken userToken);
        Task<ResultDto> DeleteUserTokenAsync(UserToken userToken);
        Task<ResultDto> SaveChangesAsync();
    }
}

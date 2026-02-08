using Domain.Entities.UserManagement;

namespace Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository
{
    //interface for all User Command services
    public interface IUserRepository_Command
    {
        Task CreateUserAsync(User user, UserInRole userInRole);   //Add User and Users UserInRole for Registeration
        Task AddUserTokenAsync(UserToken userToken);
        Task DeleteUserTokenAsync(UserToken userToken);   //Delete UserToken (Not Soft Delete)
        Task AddUserLogOutVersion(Guid userId);  //when user LogOut ++ its UserLogOutVersion
        Task DeleteUserInRoleAsync(UserInRole userInRole);   //Delete UserInRole (Not Soft Delete)
        Task AddUserInRoleAsync(UserInRole userInRole);
        Task DeleteChangePasswordTokensAsync(Guid userId);   //delete all tempPassword and changePasswordConfirmation tokens
        Task AddChangePasswordTokensAsync(List<UserToken> changePasswordTokens);
        Task SaveChangesAsync();
    }
}

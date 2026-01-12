using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Persistence.DatabaseManagement.ExceptionHandler.DatabaseExceptionHandler;

namespace Persistence.ServiceRepository.Commands.UserManagementRepository
{
    //implemented class to centeralize all User table Command (Create, Update, Delete)  methods
    public class UserRepository_Command : IUserRepository_Command
    {
        private readonly IDatabaseContext _databaseContext;
        public UserRepository_Command(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task CreateUserAsync(User user, UserInRole userInRole)
        {
            try
            {
                await _databaseContext.Users.AddAsync(user);   //add User
                await _databaseContext.UserInRoles.AddAsync(userInRole);   //assign Role to User
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);   //custom handler to return some Exception with ResultDto output
            }
        }

        public async Task AddUserTokenAsync(UserToken userToken)
        {
            try
            {
                //delete all token with given TokenType and UserId
                await _databaseContext.UserTokens
                    .Where(ut => ut.UserId == userToken.UserId &&
                    ut.UserTokenType == userToken.UserTokenType)
                    .ExecuteDeleteAsync();

                //add new given Token
                await _databaseContext.UserTokens.AddAsync(userToken);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);   //custom handler to return some Exception
            }
        }

        public async Task DeleteUserTokenAsync(UserToken userToken)
        {
            try
            {
                await _databaseContext.UserTokens
                    .Where(ut => ut.UserTokenId == userToken.UserTokenId)
                    .ExecuteDeleteAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);   //custom handler to return some Exception
            }
        }

        public async Task AddUserLogOutVersion(Guid userId)
        {
            try
            {
                //add 1 to user logOut version counter to check with jwt and unauthorized on logOut
                await _databaseContext.Users
                    .Where(u => u.UserId == userId)
                    .ExecuteUpdateAsync(ex =>
                        ex.SetProperty(u => u.UserLogOutVersion, u => u.UserLogOutVersion + 1));
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);   //custom handler to return some Exception
            }
        }
    }
}

using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.ExceptionHandler.DatabaseExceptionHandler;
using Persistence.Interface.DatabaseManagement.DatabaseConfiguration;

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
                    .Where(ut => ut.UserTokenId == userToken.UserId)
                    .ExecuteDeleteAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);   //custom handler to return some Exception
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

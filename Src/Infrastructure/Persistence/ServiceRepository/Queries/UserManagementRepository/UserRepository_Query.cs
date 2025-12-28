using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Common.UserTokenType;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.ExceptionHandler.DatabaseExceptionHandler;
using Persistence.Interface.DatabaseManagement.DatabaseConfiguration;

namespace Persistence.ServiceRepository.Queries.UserManagementRepository
{
    //implemented class to centeralize all User table Query (Select)  methods
    public class UserRepository_Query : IUserRepository_Query
    {
        private readonly IDatabaseContext _databaseContext;
        public UserRepository_Query(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> CheckEmailExistAsync(string userEmail)
        {
            try
            {
                var user = await _databaseContext.Users
                .IgnoreQueryFilters()   //search in all users even soft deleted
                .AnyAsync(u => u.UserEmail == userEmail);

                return user;
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return true;
            }
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await _databaseContext.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

                return user;
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<UserToken?> GetEmailConfirmationTokenValueAsync(Guid userId)
        {
            try
            {
                //get ConfirmationEmail Token which has not expired and belongs to given UserId
                var token = await _databaseContext.UserTokens
                    .Where(ut => ut.UserId == userId &&
                    ut.UserTokenType == nameof(UserTokenType.EmailConfirmation) &&
                    ut.UserTokenIsExpired == false)
                    .OrderBy(ut => ut.CreatedAt)
                    .LastAsync();

                return token;
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }
    }
}

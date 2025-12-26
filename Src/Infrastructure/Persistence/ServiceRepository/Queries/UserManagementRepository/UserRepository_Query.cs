using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Common.UserTokenType;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseManagement.DatabaseConfiguration;

namespace Persistence.ServiceRepository.Queries.UserManagementRepository
{
    //implemented class to centeralize all User table Query (Select)  methods
    public class UserRepository_Query : IUserRepository_Query
    {
        private readonly DatabaseContext _databaseContext;
        public UserRepository_Query(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<bool> CheckEmailExistAsync(string userEmail)
        {
            var user = await _databaseContext.Users
                .IgnoreQueryFilters()   //search in all users even soft deleted
                .AnyAsync(u => u.UserEmail == userEmail);

            return user;
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            var user = await _databaseContext.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user;
        }

        public async Task<UserToken?> GetEmailConfirmationTokenValueAsync(Guid userId)
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
    }
}

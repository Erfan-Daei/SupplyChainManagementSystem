using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Persistence.DatabaseManagement.ExceptionHandler.DatabaseExceptionHandler;

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

        public async Task<User?> GetUserByEmailAsync(string userEmail)
        {
            try
            {
                return await _databaseContext.Users.FirstOrDefaultAsync(u => u.UserEmail == userEmail);
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);

                return null;
            }
        }

        public async Task<Role?> GetUserRoleByUserIdAsync(Guid userId)
        {
            try
            {
                //join role and userInRole to get role form userId
                var role = await (from ur in _databaseContext.UserInRoles
                                  join r in _databaseContext.Roles
                                  on ur.RoleId equals r.RoleId
                                  where ur.UserId == userId
                                  select r)
                                  .FirstOrDefaultAsync();
                return role;
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<UserToken?> GetUserTokenByUserIdAsync(Guid userId)
        {
            try
            {
                return await _databaseContext.UserTokens.FirstOrDefaultAsync(ut => ut.UserId == userId && ut.UserTokenType == UserTokenType.RefreshToken.ToString());
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<Guid> GetUserCompanyIdAsync(Guid userId)
        {
            try
            {
                var user = await _databaseContext.Users.Where(u => u.UserId == userId)
                    .FirstOrDefaultAsync();

                return user?.UserCompanyId ?? Guid.Empty;
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return Guid.Empty;
            }
        }

        public async Task<User?> GetUserWithUserInRoleByUserIdAsync(Guid userId)
        {
            try
            {
                return await _databaseContext.Users.Where(u => u.UserId == userId)
                    .Include(u => u.UserInRole)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<UserToken?> GetUserTokenByRefreshTokenAsync(string hashedToken)
        {
            try
            {
                return await _databaseContext.UserTokens.FirstOrDefaultAsync(ut => ut.UserTokenValue == hashedToken);
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<List<User>?> GetAllUsersByCompanyId(Guid companyId)
        {
            try
            {
                return await _databaseContext.Users.Where(u => u.UserCompanyId == companyId)
                    .Include(u => u.UserInRole)
                    .Where(u => u.UserInRole.RoleId != SeedRoles.AdminId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<User?> GetUserDetailByIdAsync(Guid userId)
        {
            try
            {
                return await _databaseContext.Users.Where(u => u.UserId == userId)
                    .Include(u => u.UserInRole)
                    .ThenInclude(ut => ut.Role)
                    .Include(u => u.UserCompany)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }
    }
}

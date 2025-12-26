using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Common.Output;
using Domain.Entities.UserManagement;
using Persistence.DatabaseManagement.DatabaseConfiguration;

namespace Persistence.ServiceRepository.Commands.UserManagementRepository
{
    //implemented class to centeralize all User table Command (Create, Update, Delete)  methods
    public class UserRepository_Command : IUserRepository_Command
    {
        private readonly DatabaseContext _databaseContext;
        public UserRepository_Command(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<ResultDto> CreateUserAsync(User user, UserInRole userInRole)
        {
            try
            {
                await _databaseContext.Users.AddAsync(user);   //add User
                await _databaseContext.UserInRoles.AddAsync(userInRole);   //assign Role to User
                await _databaseContext.SaveChangesAsync();

                return new ResultDto()
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.DatabaseExceptionHandler.Handle(ex);   //custom handler to return some Exception with ResultDto output
            }
        }

        public async Task<ResultDto> AddUserTokenAsync(UserToken userToken)
        {
            try
            {
                await _databaseContext.UserTokens.AddAsync(userToken);
                await _databaseContext.SaveChangesAsync();
                return new ResultDto()
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.DatabaseExceptionHandler.Handle(ex);   //custom handler to return some Exception with ResultDto output
            }
        }

        public async Task<ResultDto> DeleteUserTokenAsync(UserToken userToken)
        {
            try
            {
                userToken.SetDeletedAt();   //soft delete UserToken
                await _databaseContext.SaveChangesAsync();
                return new ResultDto()
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.DatabaseExceptionHandler.Handle(ex);   //custom handler to return some Exception with ResultDto output
            }
        }

        public async Task<ResultDto> SaveChangesAsync()
        {
            try
            {
                await _databaseContext.SaveChangesAsync();
                return new ResultDto()
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.DatabaseExceptionHandler.Handle(ex);   //custom handler to return some Exception with ResultDto output
            }
        }
    }
}

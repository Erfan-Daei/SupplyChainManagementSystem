using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.HashManagement;
using Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.VerifyChangePassword;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Commands.Users.UserManagement.ChangePassword.VerifyChangePassword
{
    public class VerifyChangePasswordService : IVerifyChangePassword
    {
        private readonly IUserRepository_Query _user_Query;   //GetUserByIdAsync   GetUserChangePasswordTokenAsync   GetUserTempPasswordAsync
        private readonly IUserRepository_Command _user_Command;   //SaveChangesAsync
        private readonly IHashManager _hashManager;   //BCryptVerifyHashedValue
        public VerifyChangePasswordService(IUserRepository_Query user_Query
            , IUserRepository_Command user_Command
            , IHashManager hashManager)
        {
            _user_Query = user_Query;
            _user_Command = user_Command;
            _hashManager = hashManager;
        }
        public async Task<ResultDto> VerifyChangePasswordAsync(VerifyChangePasswordCommandRequest request, CancellationToken ct)
        {
            try
            {
                var user = await _user_Query.GetUserByIdAsync(request.userId ?? Guid.Empty);
                if (user == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.UserNotFound, HttpStatusCode.NotFound);

                var hashedChangePasswordToken = await _user_Query.GetUserChangePasswordTokenAsync(user.UserId);
                if (hashedChangePasswordToken == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.TokenExpiredOrNotFount, HttpStatusCode.NotFound);

                if (hashedChangePasswordToken.CheckIsExpired())
                    return ResultDto.Failed(ResultDtoMessageLibrary.TokenExpiredOrNotFount, HttpStatusCode.NotFound);

                var checkToken = _hashManager.BCryptVerifyHashedValue(request.token, hashedChangePasswordToken.UserTokenValue);
                if (!checkToken)
                    return ResultDto.Failed(ResultDtoMessageLibrary.InvalidToken, HttpStatusCode.BadRequest);

                var tempPasswordToken = await _user_Query.GetUserTempPasswordAsync(user.UserId);
                if (tempPasswordToken == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.UnExpectedErrorOccured, HttpStatusCode.InternalServerError);

                user.UpdatePassword(tempPasswordToken.UserTokenValue);
                await _user_Command.SaveChangesAsync();

                return ResultDto.Succeeded(ResultDtoMessageLibrary.UserPasswordUpdated, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

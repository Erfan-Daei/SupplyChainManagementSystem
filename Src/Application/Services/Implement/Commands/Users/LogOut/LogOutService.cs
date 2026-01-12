using Application.Dtos.JWT;
using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.HashManagement;
using Application.Services.MediatR.Commands.User.LogOut;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Commands.Users.LogOut
{
    public class LogOutService : ILogOut
    {
        private readonly IUserRepository_Query _user_Query;   //GetUserTokenByRefreshTokenAsync
        private readonly IUserRepository_Command _user_Command;   //DeleteUserTokenAsync   AddUserLogOutVersion
        private readonly IHashManager _hashManager;   //HashPassword
        private readonly RefreshTokenSettings _refreshTokenSettings;
        public LogOutService(IUserRepository_Query user_Query,
            IUserRepository_Command user_Command,
            IHashManager hashManager,
            RefreshTokenSettings refreshTokenSettings)
        {
            _user_Query = user_Query;
            _user_Command = user_Command;
            _hashManager = hashManager;
            _refreshTokenSettings = refreshTokenSettings;
        }
        public async Task<ResultDto> LogOutAsync(LogOutCommand request, CancellationToken ct)
        {
            try
            {
                //hash token to find userToken from it
                var hashedRefreshToken = _hashManager.HMACSHA256HashValue(_refreshTokenSettings.SecretKey, request.refreshToken);

                //get userToken by hashed input
                var userToken = await _user_Query.GetUserTokenByRefreshTokenAsync(hashedRefreshToken);
                if (userToken == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.InvalidToken, HttpStatusCode.Unauthorized);

                //check if token is expired
                var checkTokenExpiration = userToken.CheckIsExpired();
                if (checkTokenExpiration)
                {
                    await _user_Command.DeleteUserTokenAsync(userToken);

                    return ResultDto.Failed(ResultDtoMessageLibrary.TokenExpired, HttpStatusCode.Unauthorized);
                }

                //add 1 to user logOut version counter to check with jwt and unauthorized on logOut
                await _user_Command.AddUserLogOutVersion(userToken.UserId);

                //delete refresh token 
                await _user_Command.DeleteUserTokenAsync(userToken);

                return ResultDto.Succeeded(ResultDtoMessageLibrary.LoggedOut, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

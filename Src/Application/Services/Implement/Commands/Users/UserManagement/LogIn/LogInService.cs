using Application.Dtos.JWT;
using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.HashManagement;
using Application.Interfaces.JWT;
using Application.Services.MediatR.Commands.User.UserManagement.LogIn;
using Common.Output;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Users.UserManagement.LogIn
{
    public class LogInService : ILogIn
    {
        private readonly IUserRepository_Query _user_Query;   //GetUserByEmailAsync   GetUserRoleByUserIdAsync
        private readonly IUserRepository_Command _user_Command;   //AddUserTokenAsync
        private readonly IHashManager _hashManager;   //VerifyHashedValue   GenerateHashedToken
        private readonly IJwtTokenManager _jwtTokenManager;   //GenerateToken
        private readonly RefreshTokenSettings _refreshTokenSettings;   //RefreshToken ExpireTime
        public LogInService(IUserRepository_Query user_Query,
            IUserRepository_Command user_Command,
            IHashManager hashManager,
            IJwtTokenManager jwtTokenManager,
            RefreshTokenSettings refreshTokenSettings)
        {
            _user_Query = user_Query;
            _user_Command = user_Command;
            _hashManager = hashManager;
            _jwtTokenManager = jwtTokenManager;
            _refreshTokenSettings = refreshTokenSettings;
        }

        public async Task<ResultDto<LogInServiceResultDto>> LogInAsync(LogInCommand request, CancellationToken ct)
        {
            try
            {
                //get user
                var user = await _user_Query.GetUserByEmailAsync(request.UserEmail);
                if (user == null)
                    return ResultDto<LogInServiceResultDto>.Failed(ResultDtoMessageLibrary.WrongUsernameOrPassword, HttpStatusCode.NotFound);   //404

                //check given password is valid
                var VerifyPassword = _hashManager.BCryptVerifyHashedValue(request.UserPassword, user.UserPassword);
                if (!VerifyPassword)
                    return ResultDto<LogInServiceResultDto>.Failed(ResultDtoMessageLibrary.WrongUsernameOrPassword, HttpStatusCode.Unauthorized);   //401

                //get user role
                var role = await _user_Query.GetUserRoleByUserIdAsync(user.UserId);
                if (role == null)
                    return ResultDto<LogInServiceResultDto>.Failed(ResultDtoMessageLibrary.UserRoleNotFound, HttpStatusCode.NotFound);   //404

                //create Jwt token
                var token = _jwtTokenManager.GenerateToken(user, role);
                if (token == null)
                    return ResultDto<LogInServiceResultDto>.Failed(ResultDtoMessageLibrary.UnExpectedErrorOccured, HttpStatusCode.InternalServerError);

                //create Refresh token (plain and hashed)
                var refreshToken = _hashManager.HMACSHA256GenerateHashedToken(_refreshTokenSettings.SecretKey);

                var userToken = UserToken.Create(refreshToken.hashed, UserTokenType.RefreshToken.ToString(), _refreshTokenSettings.ExpireDays, user.UserId);

                await _user_Command.AddUserTokenAsync(userToken);

                return ResultDto<LogInServiceResultDto>.Succeeded(new LogInServiceResultDto
                {
                    AccessToken = token,
                    RefreshToken = refreshToken.plain,
                    RefreshTokenExpireTime = userToken.UserTokenExpireTime,
                }, ResultDtoMessageLibrary.LoggedIn, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<LogInServiceResultDto>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
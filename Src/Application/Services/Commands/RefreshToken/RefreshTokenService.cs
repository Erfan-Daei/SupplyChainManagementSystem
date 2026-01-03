using Application.Dtos.JWT;
using Application.Dtos.Services.Commands.RefreshToken;
using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.HashManagement;
using Application.Interfaces.JWT;
using Application.Interfaces.Services.Commands.RefreshToken;
using Application.MediatR.Services.Commands.RefreshToken;
using Common.Output;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Commands.RefreshToken
{
    public class RefreshTokenService : IRefreshToken
    {
        private readonly IHashManager _hashManager;   //HMACSHA256HashValue   HMACSHA256GenerateHashedToken
        private readonly RefreshTokenSettings _refreshTokenSettings;   //SecretKey   ExpireDays
        private readonly IUserRepository_Query _user_Query;   //GetUserTokenByRefreshTokenAsync
        private readonly IJwtTokenManager _jwtTokenManager;   //GenerateToken
        private readonly IUserRepository_Command _user_Command;   //AddUserTokenAsync
        public RefreshTokenService(IHashManager hashManager,
            RefreshTokenSettings refreshTokenSettings,
            IUserRepository_Query user_Query,
            IJwtTokenManager jwtTokenManager,
            IUserRepository_Command user_Command)
        {
            _hashManager = hashManager;
            _refreshTokenSettings = refreshTokenSettings;
            _user_Query = user_Query;
            _jwtTokenManager = jwtTokenManager;
            _user_Command = user_Command;
        }
        public async Task<ResultDto<RefreshTokenServiceResultDto>> GenerateRefreshTokenAsync(RefreshTokenCommand request, CancellationToken ct)
        {
            try
            {
                //hash refreshToken
                var hashedRefreshToken = _hashManager.HMACSHA256HashValue(_refreshTokenSettings.SecretKey, request.refreshToken);

                //check userToken in Database by hashedRefreshToken
                var userToken = await _user_Query.GetUserTokenByRefreshTokenAsync(hashedRefreshToken);
                if (userToken == null)
                    return ResultDto<RefreshTokenServiceResultDto>.Failed("توکن نامعتبر است", HttpStatusCode.Unauthorized);

                //check Token is Expired or not
                if (userToken.CheckIsExpired())
                    return ResultDto<RefreshTokenServiceResultDto>.Failed("توکن منقضی شده است", HttpStatusCode.Unauthorized);

                //get User
                var user = await _user_Query.GetUserByIdAsync(userToken.UserId);
                if (user == null)
                    return ResultDto<RefreshTokenServiceResultDto>.Failed("کاربر یافت نشد", HttpStatusCode.NotFound);

                //find userRole
                var userRole = await _user_Query.GetUserRoleByUserIdAsync(user.UserId);
                if (userRole == null)
                    return ResultDto<RefreshTokenServiceResultDto>.Failed("نقش کاربر یافت نشد", HttpStatusCode.Unauthorized);

                //generate JwT token
                var jwtToken = _jwtTokenManager.GenerateToken(user, userRole);

                //generate refreshToken
                var newRefreshToken = _hashManager.HMACSHA256GenerateHashedToken(_refreshTokenSettings.SecretKey);

                //create new UserToken
                var newUserToken = UserToken.Create(newRefreshToken.hashed, UserTokenType.RefreshToken.ToString(), _refreshTokenSettings.ExpireDays, user.UserId);

                //add new refreshToken to database
                await _user_Command.AddUserTokenAsync(newUserToken);

                return ResultDto<RefreshTokenServiceResultDto>
                    .Succeeded(new RefreshTokenServiceResultDto
                    {
                        AccessToken = jwtToken,
                        RefreshToken = newRefreshToken.plain,
                        RefreshTokenExpirationTime = newUserToken.UserTokenExpireTime
                    }, "توکن با موفقیت بازسازی شد", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<RefreshTokenServiceResultDto>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.JWT;
using Application.Services.MediatR.Commands.User.UserManagement.LogOut;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Commands.Users.UserManagement.LogOut
{
    public class LogOutService : ILogOut
    {
        private readonly IUserRepository_Query _user_Query;   //GetRefreshTokenByUserIdAsync
        private readonly IUserRepository_Command _user_Command;   //DeleteUserTokenAsync   AddUserLogOutVersion
        private readonly IJwtTokenManager _jwtTokenManager;   //GetUserId
        public LogOutService(IUserRepository_Query user_Query
            , IUserRepository_Command user_Command
            , IJwtTokenManager jwtTokenManager)
        {
            _user_Query = user_Query;
            _user_Command = user_Command;
            _jwtTokenManager = jwtTokenManager;
        }
        public async Task<ResultDto> LogOutAsync(LogOutCommand request, CancellationToken ct)
        {
            try
            {
                var userId = Guid.Empty;

                if (!string.IsNullOrEmpty(request.commandRequest.userId))
                    userId = Guid.Parse(request.commandRequest.userId);

                if (userId == Guid.Empty)
                    userId = _jwtTokenManager.GetUserId(request.userClaims);

                else if (userId == Guid.Empty)
                    return ResultDto.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Unauthorized);

                //get userToken by hashed input
                var userToken = await _user_Query.GetRefreshTokenByUserIdAsync(userId);
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

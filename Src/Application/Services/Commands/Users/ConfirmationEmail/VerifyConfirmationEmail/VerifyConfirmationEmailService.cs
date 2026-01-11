using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.HashManagement;
using Application.Interfaces.Services.Commands.User.ConfirmationEmail;
using Common.Output;
using System.Net;

namespace Application.Services.Commands.Users.ConfirmationEmail.VerifyConfirmationEmail
{
    public class VerifyConfirmationEmailService : IVerifyConfirmationEmail
    {
        private readonly IUserRepository_Command _user_Command;   //SaveChangesAsync
        private readonly IUserRepository_Query _user_Query;   //GetUserByIdAsync   GetEmailConfirmationTokenValueAsync
        private readonly IHashManager _hashManager;   //VerifyHashedValue
        public VerifyConfirmationEmailService
        (
            IUserRepository_Command user_Command,
            IUserRepository_Query user_Query,
            IHashManager hashManager
        )
        {
            _user_Command = user_Command;
            _user_Query = user_Query;
            _hashManager = hashManager;
        }
        public async Task<ResultDto> VerifyConfirmationEmailAsync(Guid userId, string plainToken, CancellationToken ct)
        {
            try
            {
                //get User
                var user = await _user_Query.GetUserByIdAsync(userId);
                if (user == null)
                    return ResultDto.Failed("کاربر یافت نشد", HttpStatusCode.NotFound);

                //get UserTokrn
                var userToken = await _user_Query.GetEmailConfirmationTokenValueAsync(userId);
                if (userToken == null)
                    return ResultDto.Failed("توکن کاربر منقضی شده یا وجود ندارد", HttpStatusCode.NotFound);

                //check if UserToken is expired or not
                if (userToken.CheckIsExpired())
                {
                    await _user_Command.DeleteUserTokenAsync(userToken);

                    return ResultDto.Failed("توکن کاربر منقضی شده", HttpStatusCode.Unauthorized);
                }

                var verifyTokenResult = _hashManager.BCryptVerifyHashedValue(plainToken, userToken.UserTokenValue);
                if (!verifyTokenResult)
                    return ResultDto.Failed("توکن ورودی اشتباه است", HttpStatusCode.BadRequest);

                userToken.SetIsUsed();
                user.ChangeUserEmailConfirmedState();

                await _user_Command.SaveChangesAsync();

                return ResultDto.Succeeded("ایمیل شما با موفقیت تایید شد", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

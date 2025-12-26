using Application.Interfaces.EmailManagement;
using Application.Interfaces.Services.Commands.ConfirmationEmail;
using Common.Output;
using Common.UserTokenType;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Commands.ConfirmationEmail
{
    //class to mange Confirmation Email process
    public class SendConfirmationEmailService : ISendConfirmationEmail
    {
        //centeralized class to contain all dependencies
        private readonly SendConfirmationEmailServiceDependency _dependency;
        public SendConfirmationEmailService(SendConfirmationEmailServiceDependency dependency)
        {
            _dependency = dependency;
        }
        public async Task<ResultDto> SendConfirmationEmail(Guid userId)
        {
            //get User
            var user = await _dependency.user_Query.GetUserByIdAsync(userId);
            if (user == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کابر یافت نشد",
                    StatusCode = HttpStatusCode.NotFound   // 404
                };
            }
            ;

            //generate Plain for Email and Hashed for database Token
            var tokens = _dependency.hashManager.GenerateHashedToken();

            //get confirmationEmailSettings from appsettings.json
            var confirmationEmailSettings = _dependency.confirmationEmailSettings;

            UserToken userToken = new UserToken()
            {
                UserTokenId = Guid.NewGuid(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UserTokenType = nameof(UserTokenType.EmailConfirmation),
                UserTokenValue = tokens.hashed,
                UserTokenExpireTime = DateTime.UtcNow.AddMinutes(confirmationEmailSettings.UserTokenExpireMinutes)
            };

            //save hashed Token to database
            var saveTokenResult = await _dependency.user_Command.AddUserTokenAsync(userToken);
            if (!saveTokenResult.IsSuccess)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = saveTokenResult.Message,
                    StatusCode = saveTokenResult.StatusCode
                };
            }

            //send plain token with Email for confirmation
            
            var sendEmailResult = await _dependency.emailSender.ConfirmationEmailSenderAsync(new ConfirmationEmailSenderRequestDto
            {
                UserEmail = user.UserEmail,
                ActivationLink = confirmationEmailSettings.ActivationLink
                .Replace("{Token}", tokens.plain),
                UserFullName = user.UserFullName,
                Subject = confirmationEmailSettings.Subject,
            });
            if (!sendEmailResult.IsSuccess)
            {
                //if Email sending has a problem soft delete this UserToken
                var DeleteUserTokenResult = await _dependency.user_Command.DeleteUserTokenAsync(userToken);
                if (!DeleteUserTokenResult.IsSuccess)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "حذف توکن کاربر با مشکل مواجه شده" + DeleteUserTokenResult.Message,
                        StatusCode = DeleteUserTokenResult.StatusCode
                    };
                }
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = sendEmailResult.Message,
                    StatusCode = sendEmailResult.StatusCode
                };
            }

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "ایمیل تایید به حساب شما ارسال گردید",
                StatusCode = HttpStatusCode.OK   // 200
            };
        }
    }
}

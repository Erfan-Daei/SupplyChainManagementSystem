using Application.Dtos.EmailManagement;
using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.EmailManagement;
using Application.Interfaces.HashManagement;
using Application.Interfaces.Services.Commands.ConfirmationEmail;
using Common.Output;
using Common.UserTokenType;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Commands.ConfirmationEmail.SendConfirmationEmail
{
    //class to mange Confirmation Email process
    public class SendConfirmationEmailService : ISendConfirmationEmail
    {
        private readonly IUserRepository_Command _user_Command;   //AddUserTokenAsync   DeleteUserTokenAsync
        private readonly IUserRepository_Query _user_Query;   //GetUserByIdAsync
        private readonly IHashManager _hashManager;   //hashManager
        private readonly IEmailManager _emailSender;   //ConfirmationEmailSenderAsync
        private readonly ConfirmationEmailSettings _confirmationEmailSettings;   //ConfirmationEmailSettings
        public SendConfirmationEmailService(IUserRepository_Command user_Command,
            IUserRepository_Query user_Query,
            IHashManager hashManager,
            IEmailManager emailSender,
            ConfirmationEmailSettings confirmationEmailSettings)
        {
            _user_Command = user_Command;
            _user_Query = user_Query;
            _hashManager = hashManager;
            _emailSender = emailSender;
            _confirmationEmailSettings = confirmationEmailSettings;
        }
        public async Task<ResultDto> SendConfirmationEmail(Guid userId)
        {
            try
            {
                if (Guid.Empty == userId)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "لطفا آی دی کاربر را به درستی وارد کنید",
                        StatusCode = HttpStatusCode.BadRequest   // 400
                    };
                }

                //get User
                var user = await _user_Query.GetUserByIdAsync(userId);
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
                var tokens = _hashManager.GenerateHashedToken();

                //get confirmationEmailSettings from appsettings.json
                var confirmationEmailSettings = _confirmationEmailSettings;

                //creator of UserToken
                var userToken = UserToken.Create(tokens.hashed,
                    nameof(UserTokenType.EmailConfirmation),
                    confirmationEmailSettings.UserTokenExpireMinutes,
                    userId);

                //save hashed Token to database
                await _user_Command.AddUserTokenAsync(userToken);
                
                //send plain token with Email for confirmation

                var sendEmailResult = await _emailSender.ConfirmationEmailSenderAsync(new ConfirmationEmailSenderRequestDto
                {
                    UserEmail = user.UserEmail,
                    ActivationLink = confirmationEmailSettings.ActivationLink
                    .Replace("{UserId}", userId.ToString())
                    .Replace("{Token}", tokens.plain),
                    UserFullName = user.UserFullName,
                    Subject = confirmationEmailSettings.Subject,
                });
                if (!sendEmailResult.IsSuccess)
                {
                    //if Email sending has a problem soft delete this UserToken
                    await _user_Command.DeleteUserTokenAsync(userToken);
                    
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
            catch (Exception ex)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message= ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}

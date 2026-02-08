using Application.Dtos.EmailManagement;
using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.EmailManagement;
using Application.Interfaces.HashManagement;
using Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.ChangePasswordConfirmation.SendChangePasswordConfirmation;
using Common.Output;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Users.UserManagement.ChangePassword.ChangePasswordConfirmation.SendChangePasswordConfirmation
{
    public class SendChangePasswordConfirmationService : ISendChangePasswordConfirmation
    {
        private readonly IUserRepository_Query _user_Query;   //GetUserByIdAsync
        private readonly IHashManager _hashManager;   //BCryptHashPassword
        private readonly IUserRepository_Command _user_Command;   //AddUserTokenAsync   DeleteUserTokenAsync
        private readonly ChangePasswordConfirmationSettings _changePasswordConfirmationSettings;   //ChangePasswordConfirmationSettings
        private readonly IEmailManager _emailSender;
        public SendChangePasswordConfirmationService(IUserRepository_Query user_Query
            , IHashManager hashManager
            , IUserRepository_Command user_Command
            , ChangePasswordConfirmationSettings changePasswordConfirmationSettings
            , IEmailManager emailSender)
        {
            _user_Query = user_Query;
            _hashManager = hashManager;
            _user_Command = user_Command;
            _changePasswordConfirmationSettings = changePasswordConfirmationSettings;
            _emailSender = emailSender;
        }
        public async Task<ResultDto> SendChangePasswordConfirmationAsync(SendChangePasswordConfirmationCommandRequest request, CancellationToken ct)
        {
            try
            {
                var user = await _user_Query.GetUserByIdAsync(request.userId ?? Guid.Empty);
                if (user == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.UserNotFound, HttpStatusCode.NotFound);

                var plain6Digit = new Random().Next(100000, 999999);
                var hashedDigit = _hashManager.BCryptHashPassword(plain6Digit.ToString());

                var userToken = UserToken.Create
                (
                    hashedDigit,
                    UserTokenType.ChangePassword.ToString(),
                    _changePasswordConfirmationSettings.UserTokenExpireMinutes,
                    user.UserId
                );

                await _user_Command.AddUserTokenAsync(userToken);

                var sendEmailResult = await _emailSender.ConfirmationEmailSenderAsync(new ConfirmationEmailSenderRequestDto
                {
                    Subject = _changePasswordConfirmationSettings.Subject,
                    Value = plain6Digit.ToString(),
                    UserEmail = user.UserEmail,
                    UserFullName = user.UserFullName,
                    EmailSenderType = ConfirmationEmailSenderType.ChangePasswordConfirmation
                });

                if (!sendEmailResult.IsSuccess)
                {
                    //if Email sending has a problem delete this UserToken
                    await _user_Command.DeleteUserTokenAsync(userToken);

                    return ResultDto.Failed(sendEmailResult.Message, sendEmailResult.StatusCode);
                }

                return ResultDto.Succeeded(ResultDtoMessageLibrary.ConfirmationEmailSent, HttpStatusCode.Accepted);

            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.EmailManagement;
using Application.Interfaces.HashManagement;

namespace Application.Services.Commands.ConfirmationEmail.SendConfirmationEmail
{
    //side class to contain all SendConfirmationEmailServic Dependencies
    public class SendConfirmationEmailServiceDependency
    {
        public IUserRepository_Command user_Command { get; }   //AddUserTokenAsync   DeleteUserTokenAsync
        public IUserRepository_Query user_Query { get; }   //GetUserByIdAsync
        public IHashManager hashManager { get; }   //hashManager
        public IEmailManager emailSender { get; }   //ConfirmationEmailSenderAsync
        public ConfirmationEmailSettings confirmationEmailSettings { get; }   //ConfirmationEmailSettings
        public SendConfirmationEmailServiceDependency(IUserRepository_Command _user_Command,
            IUserRepository_Query _user_Query,
            IHashManager _hashManager,
            IEmailManager _emailSender,
            ConfirmationEmailSettings _confirmationEmailSettings)
        {
            user_Command = _user_Command;
            user_Query = _user_Query;
            hashManager = _hashManager;
            emailSender = _emailSender;
            confirmationEmailSettings = _confirmationEmailSettings;
        }
    }
}

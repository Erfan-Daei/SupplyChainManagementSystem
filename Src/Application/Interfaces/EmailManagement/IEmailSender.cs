using Common.Output;

namespace Application.Interfaces.EmailManagement
{
    //interface to send Email to User
    public interface IEmailSender
    {
        Task<ResultDto> ConfirmationEmailSenderAsync(ConfirmationEmailSenderRequestDto request);
    }
}

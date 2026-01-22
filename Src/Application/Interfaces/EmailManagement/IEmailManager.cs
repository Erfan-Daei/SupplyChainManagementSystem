using Application.Dtos.EmailManagement;
using Common.Output;

namespace Application.Interfaces.EmailManagement
{
    //interface to manage all EmailToUser proccess
    public interface IEmailManager
    {
        Task<ResultDto> ConfirmationEmailSenderAsync(ConfirmationEmailSenderRequestDto request);   //Send ConfirmationEmail to User Email
    }
}

using Common.Output;

namespace Application.Interfaces.Services.Commands.ConfirmationEmail
{
    public interface ISendConfirmationEmail
    {
        Task<ResultDto> SendConfirmationEmail(Guid userId);
    }
}

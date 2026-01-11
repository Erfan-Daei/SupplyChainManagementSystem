using Common.Output;

namespace Application.Interfaces.Services.Commands.User.ConfirmationEmail
{
    public interface ISendConfirmationEmail
    {
        Task<ResultDto> SendConfirmationEmail(Guid userId, CancellationToken ct);
    }
}

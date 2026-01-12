using Common.Output;

namespace Application.Services.Implement.Commands.Users.ConfirmationEmail.SendConfirmationEmail
{
    public interface ISendConfirmationEmail
    {
        Task<ResultDto> SendConfirmationEmail(Guid userId, CancellationToken ct);
    }
}

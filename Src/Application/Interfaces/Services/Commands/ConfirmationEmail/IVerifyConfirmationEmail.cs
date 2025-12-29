using Common.Output;

namespace Application.Interfaces.Services.Commands.ConfirmationEmail
{
    public interface IVerifyConfirmationEmail
    {
        Task<ResultDto> VerifyConfirmationEmailAsync(Guid userId, string plainToken, CancellationToken ct);
    }
}

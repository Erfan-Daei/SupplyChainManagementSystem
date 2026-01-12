using Common.Output;

namespace Application.Services.Implement.Commands.Users.ConfirmationEmail.VerifyConfirmationEmail
{
    public interface IVerifyConfirmationEmail
    {
        Task<ResultDto> VerifyConfirmationEmailAsync(Guid userId, string plainToken, CancellationToken ct);
    }
}

using Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.ChangePasswordConfirmation.SendChangePasswordConfirmation;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.UserManagement.ChangePassword.ChangePasswordConfirmation.SendChangePasswordConfirmation
{
    public interface ISendChangePasswordConfirmation
    {
        Task<ResultDto> SendChangePasswordConfirmationAsync(SendChangePasswordConfirmationCommandRequest request, CancellationToken ct);
    }
}

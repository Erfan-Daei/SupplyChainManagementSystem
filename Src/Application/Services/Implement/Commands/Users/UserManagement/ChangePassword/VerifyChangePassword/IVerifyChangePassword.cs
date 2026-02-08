using Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.VerifyChangePassword;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.UserManagement.ChangePassword.VerifyChangePassword
{
    public interface IVerifyChangePassword
    {
        Task<ResultDto> VerifyChangePasswordAsync(VerifyChangePasswordCommandRequest request, CancellationToken ct);
    }
}

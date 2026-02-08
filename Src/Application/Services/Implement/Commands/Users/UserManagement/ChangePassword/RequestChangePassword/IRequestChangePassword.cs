using Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.RequestChangePassword;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.UserManagement.ChangePassword.RequestChangePassword
{
    public interface IRequestChangePassword
    {
        Task<ResultDto> RequestChangePasswordAsync(RequestChangePasswordCommandRequest request, CancellationToken ct);
    }
}

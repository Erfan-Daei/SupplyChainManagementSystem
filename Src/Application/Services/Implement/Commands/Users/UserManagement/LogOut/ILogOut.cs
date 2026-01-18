using Application.Services.MediatR.Commands.User.UserManagement.LogOut;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.UserManagement.LogOut
{
    public interface ILogOut
    {
        Task<ResultDto> LogOutAsync(LogOutCommand request, CancellationToken ct);
    }
}

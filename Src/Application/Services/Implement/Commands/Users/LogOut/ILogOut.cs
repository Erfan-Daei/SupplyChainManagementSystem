using Application.Services.MediatR.Commands.User.LogOut;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.LogOut
{
    public interface ILogOut
    {
        Task<ResultDto> LogOutAsync(LogOutCommand request, CancellationToken ct);
    }
}

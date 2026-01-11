using Application.MediatR.Services.Commands.User.LogOut;
using Common.Output;

namespace Application.Interfaces.Services.Commands.User.LogOut
{
    public interface ILogOut
    {
        Task<ResultDto> LogOutAsync(LogOutCommand request, CancellationToken ct);
    }
}

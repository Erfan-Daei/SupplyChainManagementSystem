using Application.MediatR.Services.Commands.LogOut;
using Common.Output;

namespace Application.Interfaces.Services.Commands.LogOut
{
    public interface ILogOut
    {
        Task<ResultDto> LogOutAsync(LogOutCommand request, CancellationToken ct);
    }
}

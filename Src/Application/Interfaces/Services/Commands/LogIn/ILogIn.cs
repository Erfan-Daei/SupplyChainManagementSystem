using Application.Dtos.Services.Commands.LogIn;
using Application.MediatR.Services.Commands.LogIn;
using Common.Output;

namespace Application.Interfaces.Services.Commands.LogIn
{
    public interface ILogIn
    {
        Task<ResultDto<LogInServiceResultDto>> LogInAsync(LogInCommand request, CancellationToken ct);
    }
}

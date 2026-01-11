using Application.Dtos.Services.Commands.User.LogIn;
using Application.MediatR.Services.Commands.User.LogIn;
using Common.Output;

namespace Application.Interfaces.Services.Commands.User.LogIn
{
    public interface ILogIn
    {
        Task<ResultDto<LogInServiceResultDto>> LogInAsync(LogInCommand request, CancellationToken ct);
    }
}

using Application.Services.MediatR.Commands.User.LogIn;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.LogIn
{
    public interface ILogIn
    {
        Task<ResultDto<LogInServiceResultDto>> LogInAsync(LogInCommand request, CancellationToken ct);
    }
}

using Application.Services.MediatR.Commands.User.UserManagement.LogIn;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.UserManagement.LogIn
{
    public interface ILogIn
    {
        Task<ResultDto<LogInServiceResultDto>> LogInAsync(LogInCommand request, CancellationToken ct);
    }
}

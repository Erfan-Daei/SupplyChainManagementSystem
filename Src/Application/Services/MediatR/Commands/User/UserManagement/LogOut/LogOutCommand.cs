using Application.Services.Implement.Commands.Users.UserManagement.LogOut;
using Common.Output;
using MediatR;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.User.UserManagement.LogOut
{
    //command for mediatR for LogoutService
    public record LogOutCommandRequest(string? userId) : IRequest<LogOutCommand>;
    public record LogOutCommand(LogOutCommandRequest commandRequest, IEnumerable<Claim> userClaims) : IRequest<ResultDto>;

    //commandHandler for mediatR for LogOutService
    public class LogOutCommandHandler : IRequestHandler<LogOutCommand, ResultDto>
    {
        private readonly ILogOut _logOut;
        public LogOutCommandHandler(ILogOut logOut)
        {
            _logOut = logOut;
        }
        public async Task<ResultDto> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            return await _logOut.LogOutAsync(request, cancellationToken);
        }
    }
}

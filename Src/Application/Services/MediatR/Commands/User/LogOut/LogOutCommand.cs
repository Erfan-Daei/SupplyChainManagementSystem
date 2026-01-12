using Application.Services.Implement.Commands.Users.LogOut;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Commands.User.LogOut
{
    //command for mediatR for LogoutService
    public record LogOutCommand(string refreshToken) : IRequest<ResultDto>;

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

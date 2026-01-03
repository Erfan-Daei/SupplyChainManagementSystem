using Application.Interfaces.Services.Commands.LogOut;
using Application.MediatR.Services.Commands.LogOut;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Commands.LogOut
{
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

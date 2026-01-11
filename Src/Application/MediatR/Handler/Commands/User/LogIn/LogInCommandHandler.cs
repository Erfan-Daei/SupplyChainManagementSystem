using Application.Dtos.Services.Commands.User.LogIn;
using Application.Interfaces.Services.Commands.User.LogIn;
using Application.MediatR.Services.Commands.User.LogIn;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Commands.User.LogIn
{
    //handler for MediatR For Login
    public class LogInCommandHandler : IRequestHandler<LogInCommand, ResultDto<LogInServiceResultDto>>
    {
        private readonly ILogIn _logIn;
        public LogInCommandHandler(ILogIn logIn)
        {
            _logIn = logIn;
        }
        public async Task<ResultDto<LogInServiceResultDto>> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            return await _logIn.LogInAsync(request, cancellationToken);
        }
    }
}

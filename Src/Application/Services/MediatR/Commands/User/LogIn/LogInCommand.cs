using Application.Services.Implement.Commands.Users.LogIn;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Commands.User.LogIn
{
    //record for MediatR logIn command
    public record LogInCommand(string UserEmail, string UserPassword) : IRequest<ResultDto<LogInServiceResultDto>>;

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

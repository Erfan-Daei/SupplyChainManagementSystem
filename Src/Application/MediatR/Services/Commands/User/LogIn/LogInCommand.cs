using Application.Dtos.Services.Commands.User.LogIn;
using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.User.LogIn
{
    //record for MediatR logIn command
    public record LogInCommand(string UserEmail, string UserPassword) : IRequest<ResultDto<LogInServiceResultDto>>;
}

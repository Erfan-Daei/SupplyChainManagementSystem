using Application.Dtos.Services.Commands.LogIn;
using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.LogIn
{
    //record for MediatR logIn command
    public record LogInCommand(string UserEmail, string UserPassword) : IRequest<ResultDto<LogInServiceResultDto>>;
}

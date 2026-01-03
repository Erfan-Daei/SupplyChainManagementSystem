using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.LogOut
{
    //command for mediatR for LogoutService
    public record LogOutCommand(string refreshToken) : IRequest<ResultDto>;
}

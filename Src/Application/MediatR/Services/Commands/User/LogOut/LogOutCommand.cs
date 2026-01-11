using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.User.LogOut
{
    //command for mediatR for LogoutService
    public record LogOutCommand(string refreshToken) : IRequest<ResultDto>;
}

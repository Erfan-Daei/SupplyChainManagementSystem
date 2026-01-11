using Application.Dtos.Services.Commands.User.SignUp;
using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.User.SignUp
{
    //MediatR Command for SignInService
    public record SignUpCommand(SignUpServiceRequestDto Dto) : IRequest<ResultDto<Guid>>;
}

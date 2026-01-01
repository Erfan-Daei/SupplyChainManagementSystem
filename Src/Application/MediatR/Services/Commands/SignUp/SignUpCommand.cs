using Application.Dtos.Services.Commands.SignUp;
using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.SignUp
{
    //MediatR Command for SignInService
    public record SignUpCommand(SignUpServiceRequestDto Dto) : IRequest<ResultDto<Guid>>;
}

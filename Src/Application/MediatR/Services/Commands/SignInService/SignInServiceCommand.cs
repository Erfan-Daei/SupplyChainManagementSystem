using Application.Dtos.Services.Commands.SignInService;
using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.SignInService
{
    //MediatR Command for SignInService
    public record SignInServiceCommand(SignInServiceRequestDto Dto) : IRequest<ResultDto<Guid>>;
}

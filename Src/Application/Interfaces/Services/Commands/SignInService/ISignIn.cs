using Application.MediatR.Services.Commands.SignInService;
using Common.Output;

namespace Application.Interfaces.Services.Commands.SignInService
{
    public interface ISignIn
    {
        //create User and UserInRole and then give UserId to api for confirmation proccess
        Task<ResultDto<Guid>> CreateUserAsync(SignInServiceCommand request, CancellationToken ct);
    }
}

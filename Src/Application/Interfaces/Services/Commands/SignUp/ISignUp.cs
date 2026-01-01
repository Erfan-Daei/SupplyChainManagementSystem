using Application.MediatR.Services.Commands.SignUp;
using Common.Output;

namespace Application.Interfaces.Services.Commands.SignUp
{
    public interface ISignUp
    {
        //create User and UserInRole and then give UserId to api for confirmation proccess
        Task<ResultDto<Guid>> SignUpAsync(SignUpCommand request, CancellationToken ct);
    }
}

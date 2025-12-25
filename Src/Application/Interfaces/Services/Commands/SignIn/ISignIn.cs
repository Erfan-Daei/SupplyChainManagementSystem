using Application.Services.Commands.SignIn;
using Common.Output;

namespace Application.Interfaces.Services.Commands.SignIn
{
    public interface ISignIn
    {
        //create User and UserInRole and then give UserId to api for confirmation proccess
        Task<ResultDto<Guid>> CreateUserAsync(SignInServiceRequestDto request);
    }
}

using Application.Services.MediatR.Commands.User.SignUp;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.SignUp
{
    public interface ISignUp
    {
        //create User and UserInRole and then give UserId to api for confirmation proccess
        Task<ResultDto<Guid>> SignUpAsync(SignUpCommand request, CancellationToken ct);
    }
}

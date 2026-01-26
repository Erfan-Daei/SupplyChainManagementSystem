using Application.Services.MediatR.Commands.User.UserManagement.SignUp;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.UserManagement.SignUp
{
    public interface ISignUp
    {
        //create User and UserInRole and then give UserId to api for confirmation proccess
        Task<ResultDto<Guid>> SignUpAsync(SignUpCommandRequest request, CancellationToken ct);
    }
}

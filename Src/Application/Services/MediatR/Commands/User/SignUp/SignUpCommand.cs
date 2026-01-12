using Application.Services.Implement.Commands.Users.SignUp;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Commands.User.SignUp
{
    //MediatR Command for SignInService
    public record SignUpCommand(SignUpServiceRequestDto Dto) : IRequest<ResultDto<Guid>>;

    //MediatR Handler for SignInServiceCommand
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, ResultDto<Guid>>
    {
        private readonly ISignUp _signIn;
        public SignUpCommandHandler(ISignUp signIn)
        {
            _signIn = signIn;
        }

        public async Task<ResultDto<Guid>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            return await _signIn.SignUpAsync(request, cancellationToken);
        }
    }
}

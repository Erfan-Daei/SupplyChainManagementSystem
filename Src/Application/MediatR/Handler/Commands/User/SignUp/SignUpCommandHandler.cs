using Application.Interfaces.Services.Commands.User.SignUp;
using Application.MediatR.Services.Commands.User.SignUp;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Commands.User.SignUp
{
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

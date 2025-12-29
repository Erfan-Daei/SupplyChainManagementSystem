using Application.Interfaces.Services.Commands.SignInService;
using Application.MediatR.Services.Commands.SignInService;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Commands.SignInService
{
    //MediatR Handler for SignInServiceCommand
    public class SignInServiceCommandHandler : IRequestHandler<SignInServiceCommand, ResultDto<Guid>>
    {
        private readonly ISignIn _signIn;
        public SignInServiceCommandHandler(ISignIn signIn)
        {
            _signIn = signIn;
        }

        public async Task<ResultDto<Guid>> Handle(SignInServiceCommand request, CancellationToken cancellationToken)
        {
            return await _signIn.CreateUserAsync(request, cancellationToken);
        }
    }
}

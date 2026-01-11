using Application.Interfaces.Services.Commands.User.ConfirmationEmail;
using Application.MediatR.Services.Commands.User.ConfirmationEmail.VerifyConfirmationEmail;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Commands.User.ConfirmationEmail.VerifyConfirmationEmail
{
    //MediatR Handler for VerifyConfirmationEmailCommand
    public class VerifyConfirmationEmailCommandHandler : IRequestHandler<VerifyConfirmationEmailCommand, ResultDto>
    {
        private readonly IVerifyConfirmationEmail _verifyConfirmationEmail;
        public VerifyConfirmationEmailCommandHandler(IVerifyConfirmationEmail verifyConfirmationEmail)
        {
            _verifyConfirmationEmail = verifyConfirmationEmail;
        }
        public async Task<ResultDto> Handle(VerifyConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            return await _verifyConfirmationEmail.VerifyConfirmationEmailAsync(request.userId, request.userToken, cancellationToken);
        }
    }
}

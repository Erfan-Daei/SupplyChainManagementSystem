using Application.Interfaces.Services.Commands.ConfirmationEmail;
using Application.MediatR.Services.Commands.ConfirmationEmail.VerifyConfirmationEmail;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Commands.ConfirmationEmail.VerifyConfirmationEmail
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

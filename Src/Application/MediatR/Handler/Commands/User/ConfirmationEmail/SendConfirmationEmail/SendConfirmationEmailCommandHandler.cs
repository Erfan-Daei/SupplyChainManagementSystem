using Application.Interfaces.Services.Commands.User.ConfirmationEmail;
using Application.MediatR.Services.Commands.User.ConfirmationEmail.SendConfirmationEmail;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Commands.User.ConfirmationEmail.SendConfirmationEmail
{
    //MediatR Handler for SendConfirmationEmailCommand
    public class SendConfirmationEmailCommandHandler : IRequestHandler<SendConfirmationEmailCommand, ResultDto>
    {
        private readonly ISendConfirmationEmail _sendConfirmationEmail;
        public SendConfirmationEmailCommandHandler(ISendConfirmationEmail sendConfirmationEmail)
        {
            _sendConfirmationEmail = sendConfirmationEmail;
        }
        public async Task<ResultDto> Handle(SendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            return await _sendConfirmationEmail.SendConfirmationEmail(request.userId, cancellationToken);
        }
    }
}

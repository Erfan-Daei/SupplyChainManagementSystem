using Application.Services.Implement.Commands.Users.ConfirmationEmail.SendConfirmationEmail;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Commands.User.ConfirmationEmail.SendConfirmationEmail
{
    //MediatR Command For SendConfirmationEmail
    public record SendConfirmationEmailCommand(Guid userId) : IRequest<ResultDto>;

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

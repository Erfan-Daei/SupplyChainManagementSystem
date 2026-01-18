using Application.Services.Implement.Commands.Users.UserManagement.ConfirmationEmail.VerifyConfirmationEmail;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Commands.User.UserManagement.ConfirmationEmail.VerifyConfirmationEmail
{
    //MediatR Command For VerifyConfirmationEmail
    public record VerifyConfirmationEmailCommand(Guid userId, string userToken) : IRequest<ResultDto>;

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

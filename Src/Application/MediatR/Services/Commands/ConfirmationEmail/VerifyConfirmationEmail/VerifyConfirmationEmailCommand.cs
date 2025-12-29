using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.ConfirmationEmail.VerifyConfirmationEmail
{
    //MediatR Command For VerifyConfirmationEmail
    public record VerifyConfirmationEmailCommand(Guid userId, string userToken) : IRequest<ResultDto>;
}

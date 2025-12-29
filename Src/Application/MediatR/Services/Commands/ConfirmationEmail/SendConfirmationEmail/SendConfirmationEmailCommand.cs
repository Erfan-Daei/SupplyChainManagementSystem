using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.ConfirmationEmail.SendConfirmationEmail
{
    //MediatR Command For SendConfirmationEmail
    public record SendConfirmationEmailCommand(Guid userId) : IRequest<ResultDto>;
}

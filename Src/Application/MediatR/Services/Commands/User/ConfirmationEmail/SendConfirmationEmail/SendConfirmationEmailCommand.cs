using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.User.ConfirmationEmail.SendConfirmationEmail
{
    //MediatR Command For SendConfirmationEmail
    public record SendConfirmationEmailCommand(Guid userId) : IRequest<ResultDto>;
}

using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.User.UserManagement.ConfirmationEmail.SendConfirmationEmail
{
    public class SendConfirmationEmailValidator : AbstractValidator<SendConfirmationEmailCommand>
    {
        public SendConfirmationEmailValidator()
        {
            RuleFor(r => r.userId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullUserId)
                .WithErrorCode("400");
        }
    }
}

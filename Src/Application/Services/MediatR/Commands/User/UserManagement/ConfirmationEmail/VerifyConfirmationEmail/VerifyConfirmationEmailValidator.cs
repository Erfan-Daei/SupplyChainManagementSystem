using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.User.UserManagement.ConfirmationEmail.VerifyConfirmationEmail
{
    public class VerifyConfirmationEmailValidator : AbstractValidator<VerifyConfirmationEmailCommand>
    {
        public VerifyConfirmationEmailValidator()
        {
            RuleFor(r => r.userId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullUserId)
                .WithErrorCode("400");

            RuleFor(r => r.userToken)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullToken)
                .WithErrorCode("400");
        }
    }
}

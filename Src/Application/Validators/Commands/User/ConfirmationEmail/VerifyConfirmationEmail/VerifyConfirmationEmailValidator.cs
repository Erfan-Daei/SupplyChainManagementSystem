using Application.MediatR.Services.Commands.User.ConfirmationEmail.VerifyConfirmationEmail;
using FluentValidation;

namespace Application.Validators.Commands.User.ConfirmationEmail.VerifyConfirmationEmail
{
    public class VerifyConfirmationEmailValidator : AbstractValidator<VerifyConfirmationEmailCommand>
    {
        public VerifyConfirmationEmailValidator()
        {
            RuleFor(r => r.userId)
                .NotEmpty().WithMessage("لطفا آی دی را وارد کنید")
                .WithErrorCode("400");

            RuleFor(r => r.userToken)
                .NotEmpty().WithMessage("لطفا توکن خود را وارد کنید")
                .WithErrorCode("400");
        }
    }
}

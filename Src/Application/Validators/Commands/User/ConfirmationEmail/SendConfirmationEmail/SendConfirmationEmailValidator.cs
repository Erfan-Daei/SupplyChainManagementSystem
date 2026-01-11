using Application.MediatR.Services.Commands.User.ConfirmationEmail.SendConfirmationEmail;
using FluentValidation;

namespace Application.Validators.Commands.User.ConfirmationEmail.SendConfirmationEmail
{
    public class SendConfirmationEmailValidator : AbstractValidator<SendConfirmationEmailCommand>
    {
        public SendConfirmationEmailValidator()
        {
            RuleFor(r => r.userId)
                .NotEmpty().WithMessage("لطفا آیدی را وارد کنید")
                .WithErrorCode("400");
        }
    }
}

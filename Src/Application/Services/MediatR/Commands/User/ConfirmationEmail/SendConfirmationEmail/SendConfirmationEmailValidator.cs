using FluentValidation;

namespace Application.Services.MediatR.Commands.User.ConfirmationEmail.SendConfirmationEmail
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

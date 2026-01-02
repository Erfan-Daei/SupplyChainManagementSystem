using Application.MediatR.Services.Commands.LogIn;
using FluentValidation;

namespace Application.Validators.Commands.LogIn
{
    public class LogInValidator : AbstractValidator<LogInCommand>
    {
        public LogInValidator()
        {
            RuleFor(r => r.UserEmail)
                .NotEmpty().WithMessage("لطفا ایمیل خود را وارد کنید")
                .EmailAddress().WithMessage("لطفا ایمیل خود را به درستی وارد کنید")
                .WithErrorCode("400");

            RuleFor(r => r.UserPassword)
                .NotEmpty().WithMessage("لطفا رمز عبور خودرا وارد کنکید")
                .WithErrorCode("400");
        }
    }
}

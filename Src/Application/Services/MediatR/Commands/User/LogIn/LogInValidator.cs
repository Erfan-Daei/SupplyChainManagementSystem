using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.User.LogIn
{
    public class LogInValidator : AbstractValidator<LogInCommand>
    {
        public LogInValidator()
        {
            RuleFor(r => r.UserEmail)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullEmail)
                .EmailAddress().WithMessage(FluentValidationMessageLibrary.WrongEmailType)
                .WithErrorCode("400");

            RuleFor(r => r.UserPassword)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullPassword)
                .WithErrorCode("400");
        }
    }
}

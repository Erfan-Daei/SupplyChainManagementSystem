using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.VerifyChangePassword
{
    public class VerifyChangePasswordValidator : AbstractValidator<VerifyChangePasswordCommandRequest>
    {
        public VerifyChangePasswordValidator()
        {
            RuleFor(x => x.token)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullToken)
                .WithErrorCode("400");
        }
    }
}

using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.User.UserManagement.ChangePassword.RequestChangePassword
{
    public class RequestChangePasswordValidator : AbstractValidator<RequestChangePasswordCommandRequest>
    {
        public RequestChangePasswordValidator()
        {
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullPassword)
                .MinimumLength(8).WithMessage("رمز عبور باید حداقل 8 کاراکتر باشد")
                .MaximumLength(64).WithMessage("رمز عبور نمی تواند بیشتر از 64 کاراکتر باشد")
                .Matches("[A-Z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد")
                .Matches("[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد")
                .Matches("[0-9]").WithMessage("رمز عبور باید حداقل یک عدد داشته باشد")
                .Matches(@"[@$!%*?&]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد")
                .Equal(u => u.ConPassword).WithMessage(FluentValidationMessageLibrary.PasswordAndConPasswordNotEqual)
                .WithErrorCode("400");
        }
    }
}

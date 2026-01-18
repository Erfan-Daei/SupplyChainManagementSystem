using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.User.UserManagement.SignUp
{
    public class SignUpValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpValidator()
        {
            RuleFor(u => u.Dto.UserFullName)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullFullName)
                .MaximumLength(50).WithMessage("نام و نام خانوادگی باید کم تر 50 کاراکتر باشد")
                .Matches("^[A-Za-z\\u0621-\\u063A\\u0641-\\u064A\\u067E\\u0686\\u0698\\u06AF\\u06A9\\u06CC\\s]+$").WithMessage(FluentValidationMessageLibrary.WrongFullNameType)
                .WithErrorCode("400");

            RuleFor(u => u.Dto.UserEmail)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullEmail)
                .EmailAddress().WithMessage(FluentValidationMessageLibrary.WrongEmailType)
                .MaximumLength(100).WithMessage("ایمیل نمی تواند بیش از 100 کاراکتر باشد")
                .WithErrorCode("400");

            RuleFor(x => x.Dto.Password)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullPassword)
                .MinimumLength(8).WithMessage("رمز عبور باید حداقل 8 کاراکتر باشد")
                .MaximumLength(64).WithMessage("رمز عبور نمی تواند بیشتر از 64 کاراکتر باشد")
                .Matches("[A-Z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد")
                .Matches("[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد")
                .Matches("[0-9]").WithMessage("رمز عبور باید حداقل یک عدد داشته باشد")
                .Matches(@"[@$!%*?&]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد")
                .Equal(x => x.Dto.ConPassword).WithMessage(FluentValidationMessageLibrary.PasswordAndConPasswordNotEqual)
                .WithErrorCode("400");

            RuleFor(u => u.Dto.CompanyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyId)
                .WithErrorCode("400");
        }
    }
}

using Application.MediatR.Services.Commands.User.SignUp;
using FluentValidation;

namespace Application.Validators.Commands.User.SignUp
{
    public class SignUpValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpValidator()
        {
            RuleFor(u => u.Dto.UserFullName)
                .NotEmpty().WithMessage("لطفا نام و نام خانوادگی خود را وارد کنید")
                .MaximumLength(50).WithMessage("نام و نام خانوادگی باید کم تر 50 کاراکتر باشد")
                .Matches("^[A-Za-z\\u0621-\\u063A\\u0641-\\u064A\\u067E\\u0686\\u0698\\u06AF\\u06A9\\u06CC\\s]+$").WithMessage("لطفا نام و نام خانوادگی را به درستی وارد کنید")
                .WithErrorCode("400");

            RuleFor(u => u.Dto.UserEmail)
                .NotEmpty().WithMessage("لطفا ایمیل خود را وارد کنید")
                .EmailAddress().WithMessage("لطفا ایمیل خودرا به درستی وارد کنید")
                .MaximumLength(100).WithMessage("ایمیل نمی تواند بیش از 100 کاراکتر باشد")
                .WithErrorCode("400");

            RuleFor(x => x.Dto.Password)
                .NotEmpty().WithMessage("لطفا رمزعبور را وارد کنید")
                .MinimumLength(8).WithMessage("رمز عبور باید حداقل 8 کاراکتر باشد")
                .MaximumLength(64).WithMessage("رمز عبور نمی تواند بیشتر از 64 کاراکتر باشد")
                .Matches("[A-Z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد")
                .Matches("[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد")
                .Matches("[0-9]").WithMessage("رمز عبور باید حداقل یک عدد داشته باشد")
                .Matches(@"[@$!%*?&]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد")
                .Equal(x => x.Dto.ConPassword).WithMessage("رمزعبور و تکرار آن برابر نیست")
                .WithErrorCode("400");

            RuleFor(u => u.Dto.CompanyId)
                .NotEmpty().WithMessage("لطفا آی دی شرکت خود را وارد کنید")
                .WithErrorCode("400");
        }
    }
}

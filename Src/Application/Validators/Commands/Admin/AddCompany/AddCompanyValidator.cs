using Application.MediatR.Services.Commands.Admin.AddCompany;
using FluentValidation;

namespace Application.Validators.Commands.Admin.AddCompany
{
    public class AddCompanyValidator : AbstractValidator<AddCompanyCommand>
    {
        public AddCompanyValidator()
        {
            RuleFor(c => c.companyName)
                .NotEmpty().WithMessage("لطفا نام شرکت را وارد کنید")
                .WithErrorCode("400");
        }
    }
}

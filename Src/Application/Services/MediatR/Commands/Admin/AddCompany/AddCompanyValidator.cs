using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.Admin.AddCompany
{
    public class AddCompanyValidator : AbstractValidator<AddCompanyCommand>
    {
        public AddCompanyValidator()
        {
            RuleFor(c => c.companyName)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyName)
                .WithErrorCode("400");
        }
    }
}

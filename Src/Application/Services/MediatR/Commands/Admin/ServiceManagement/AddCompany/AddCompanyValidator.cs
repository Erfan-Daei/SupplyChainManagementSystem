using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.AddCompany
{
    public class AddCompanyValidator : AbstractValidator<AddCompanyCommand>
    {
        public AddCompanyValidator()
        {
            RuleFor(c => c.companyName)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyName)
                .Matches("^[A-Za-z\\u0621-\\u063A\\u0641-\\u064A\\u067E\\u0686\\u0698\\u06AF\\u06A9\\u06CC\\s]+$").WithMessage(FluentValidationMessageLibrary.WrongCompanyNameType)
                .WithErrorCode("400");
        }
    }
}

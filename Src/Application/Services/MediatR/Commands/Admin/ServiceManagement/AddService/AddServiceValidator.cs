using FluentValidation;
using Common.Output;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.AddService
{
    public class AddServiceValidator : AbstractValidator<AddServiceCommandRequest>
    {
        public AddServiceValidator()
        {
            RuleFor(s => s.companyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyId)
                .WithErrorCode("400");

            RuleFor(s => s.serviceName)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullServiceName)
                .Matches("^[A-Za-z\\u0621-\\u063A\\u0641-\\u064A\\u067E\\u0686\\u0698\\u06AF\\u06A9\\u06CC\\s]+$").WithMessage(FluentValidationMessageLibrary.WrongServiceNameType)
                .WithErrorCode("400");

            RuleFor(s => s.serviceDescription)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullServiceDescription)
                .Matches("^[-.0-9A-Za-z\\u0621-\\u063A\\u0641-\\u064A\\u067E\\u0686\\u0698\\u06AF\\u06A9\\u06CC\\s]+$").WithMessage(FluentValidationMessageLibrary.WrongServiceDescriptionType)
                .WithErrorCode("400");
        }
    }
}

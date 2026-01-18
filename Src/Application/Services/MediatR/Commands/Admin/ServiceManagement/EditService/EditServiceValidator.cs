using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.EditService
{
    public class EditServiceValidator : AbstractValidator<EditServiceCommandRequest>
    {
        public EditServiceValidator()
        {
            RuleFor(s => s.Dto.ServiceId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullServiceId)
                .WithErrorCode("400");

            RuleFor(s => s.Dto.ServiceName)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullServiceName)
                .Matches("^[A-Za-z\\u0621-\\u063A\\u0641-\\u064A\\u067E\\u0686\\u0698\\u06AF\\u06A9\\u06CC\\s]+$").WithMessage(FluentValidationMessageLibrary.WrongServiceNameType)
                .WithErrorCode("400");

            RuleFor(s => s.Dto.ServiceDescription)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullServiceDescription)
                .Matches("^[-.0-9A-Za-z\\u0621-\\u063A\\u0641-\\u064A\\u067E\\u0686\\u0698\\u06AF\\u06A9\\u06CC\\s]+$").WithMessage(FluentValidationMessageLibrary.WrongServiceDescriptionType)
                .WithErrorCode("400");
        }
    }
}

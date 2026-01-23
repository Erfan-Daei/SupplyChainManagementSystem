using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.UnAssignServiceFromCompany
{
    public class UnAssignServiceFromCompanyValidator : AbstractValidator<UnAssignServiceFromCompanyCommandRequest>
    {
        public UnAssignServiceFromCompanyValidator()
        {
            RuleFor(c => c.companyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyId)
                .WithErrorCode("400");

            RuleFor(c => c.serviceId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullServiceId)
                .WithErrorCode("400");
        }
    }
}

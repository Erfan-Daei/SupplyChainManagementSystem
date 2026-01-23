using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.AssignServiceToCompany
{
    public class AssignServiceToCompanyValidator : AbstractValidator<AssignServiceToCompanyCommandRequest>
    {
        public AssignServiceToCompanyValidator()
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

using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.AddSupplyRelation
{
    public class AddSupplyRelationValidator : AbstractValidator<AddSupplyRelationCommandRequest>
    {
        public AddSupplyRelationValidator()
        {
            RuleFor(sr => sr.serviceId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullServiceId)
                .WithErrorCode("400");

            RuleFor(sr => sr.supplierCompanyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullSupplierCompanyId)
                .WithErrorCode("400");

            RuleFor(sr => sr.consumerCompanyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullConsumerCompanyId)
                .WithErrorCode("400");
        }
    }
}

using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.ConfirmSupplyRelation
{
    public class ConfirmSupplyRelationValidator : AbstractValidator<ConfirmSupplyRelationCommandRequest>
    {
        public ConfirmSupplyRelationValidator()
        {
            RuleFor(sr => sr.supplyRelationId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullSupplyRelationId)
                .WithErrorCode("400");
        }
    }
}

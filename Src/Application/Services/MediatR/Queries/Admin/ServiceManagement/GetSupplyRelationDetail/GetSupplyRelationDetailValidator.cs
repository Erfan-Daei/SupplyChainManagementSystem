using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationDetail
{
    public class GetSupplyRelationDetailValidator : AbstractValidator<GetSupplyRelationDetailQueryRequest>
    {
        public GetSupplyRelationDetailValidator()
        {
            RuleFor(sr => sr.supplyRelationId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullSupplyRelationId)
                .WithErrorCode("400");
        }
    }
}

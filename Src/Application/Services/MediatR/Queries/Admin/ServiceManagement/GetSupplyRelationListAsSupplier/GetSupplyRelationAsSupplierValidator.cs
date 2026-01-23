using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationListAsSupplier
{
    public class GetSupplyRelationAsSupplierValidator : AbstractValidator<GetSupplyRelationAsSupplierQueryRequest>
    {
        public GetSupplyRelationAsSupplierValidator()
        {
            RuleFor(c => c.companyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyId)
                .WithErrorCode("400");
        }
    }
}

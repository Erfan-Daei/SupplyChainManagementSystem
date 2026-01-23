using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Queries.Admin.ServiceManagement.GetSupplyRelationAsConsumer
{
    public class GetSupplyRelationAsConsumerValidator : AbstractValidator<GetSupplyRelationAsConsumerQueryRequest>
    {
        public GetSupplyRelationAsConsumerValidator()
        {
            RuleFor(c => c.companyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyId)
                .WithErrorCode("400");
        }
    }
}

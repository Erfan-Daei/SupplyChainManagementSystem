using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Queries.Users.ServiceManagement.GetUnConfirmedSupplyRelationList
{
    public class GetUnConfirmedSupplyRelationListValidator : AbstractValidator<GetUnConfirmedSupplyRelationListQueryRequest>
    {
        public GetUnConfirmedSupplyRelationListValidator()
        {
            RuleFor(c => c.companyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyId)
                .WithErrorCode("400");
        }
    }
}

using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyServiceListAsSupplier
{
    public class GetCompanyServiceListAsSupplierValidator : AbstractValidator<GetCompanyServiceListAsSupplierQuery>
    {
        public GetCompanyServiceListAsSupplierValidator()
        {
            RuleFor(s => s.companyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyId)
                .WithErrorCode("400");
        }
    }
}

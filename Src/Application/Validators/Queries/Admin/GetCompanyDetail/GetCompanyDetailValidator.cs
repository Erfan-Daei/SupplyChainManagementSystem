using Application.MediatR.Services.Queries.Admin.GetCompanyDetail;
using FluentValidation;

namespace Application.Validators.Queries.Admin.GetCompanyDetail
{
    public class GetCompanyDetailValidator : AbstractValidator<GetCompanyDetailQuery>
    {
        public GetCompanyDetailValidator()
        {
            RuleFor(c => c.companyId)
                .NotEmpty().WithMessage("آی دی شرکت نامعتبر است")
                .WithErrorCode("400");
        }
    }
}

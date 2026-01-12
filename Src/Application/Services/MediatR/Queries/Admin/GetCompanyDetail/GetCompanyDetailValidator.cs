using FluentValidation;

namespace Application.Services.MediatR.Queries.Admin.GetCompanyDetail
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

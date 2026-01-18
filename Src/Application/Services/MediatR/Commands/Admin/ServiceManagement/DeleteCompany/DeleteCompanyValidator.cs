using FluentValidation;
using Common.Output;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteCompany
{
    public class DeleteCompanyValidator : AbstractValidator<DeleteCompanyCommand>
    {
        public DeleteCompanyValidator()
        {
            RuleFor(c => c.companyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyId)
                .WithErrorCode("400");
        }
    }
}

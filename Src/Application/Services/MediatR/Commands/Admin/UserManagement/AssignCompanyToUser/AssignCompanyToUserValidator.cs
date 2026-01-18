using FluentValidation;
using Common.Output;

namespace Application.Services.MediatR.Commands.Admin.UserManagement.AssignCompanyToUser
{
    public class AssignCompanyToUserValidator : AbstractValidator<AssignCompanyToUserCommandRequest>
    {
        public AssignCompanyToUserValidator()
        {
            RuleFor(u => u.userId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullUserId)
                .WithErrorCode("400");

            RuleFor(u => u.companyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyId)
                .WithErrorCode("400");
        }
    }
}

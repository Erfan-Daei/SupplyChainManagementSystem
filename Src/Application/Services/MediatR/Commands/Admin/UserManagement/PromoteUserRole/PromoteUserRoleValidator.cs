using FluentValidation;
using Common.Output;

namespace Application.Services.MediatR.Commands.Admin.UserManagement.PromoteUserRole
{
    public class PromoteUserRoleValidator : AbstractValidator<PromoteUserRoleCommandRequest>
    {
        public PromoteUserRoleValidator()
        {
            RuleFor(u => u.userId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullUserId)
                .WithErrorCode("400");
        }
    }
}

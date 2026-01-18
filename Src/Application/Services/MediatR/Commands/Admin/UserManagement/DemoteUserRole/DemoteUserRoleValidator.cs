using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.Admin.UserManagement.DemoteUserRole
{
    public class DemoteUserRoleValidator : AbstractValidator<DemoteUserRoleCommandRequest>
    {
        public DemoteUserRoleValidator()
        {
            RuleFor(d => d.userId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullUserId)
                .WithErrorCode("400");
        }
    }
}

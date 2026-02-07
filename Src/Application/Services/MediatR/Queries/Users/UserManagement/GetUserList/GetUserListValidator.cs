using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Queries.Users.UserManagement.GetUserList
{
    public class GetUserListValidator : AbstractValidator<GetUserListQueryRequest>
    {
        public GetUserListValidator()
        {
            RuleFor(c => c.usersCompanyId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullCompanyId)
                .WithErrorCode("400");
        }
    }
}

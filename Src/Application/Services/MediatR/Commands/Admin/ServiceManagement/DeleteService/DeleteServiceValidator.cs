using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteService
{
    public class DeleteServiceValidator : AbstractValidator<DeleteServiceCommandRequest>
    {
        public DeleteServiceValidator()
        {
            RuleFor(s => s.serviceId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullServiceId)
                .WithErrorCode("400");
        }
    }
}

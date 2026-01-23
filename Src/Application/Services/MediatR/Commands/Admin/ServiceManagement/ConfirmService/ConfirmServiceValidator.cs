using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.ConfirmService
{
    public class ConfirmServiceValidator : AbstractValidator<ConfirmServiceCommand>
    {
        public ConfirmServiceValidator()
        {
            RuleFor(s => s.serviceId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullServiceId)
                .WithErrorCode("400");
        }
    }
}

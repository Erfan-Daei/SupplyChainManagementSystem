using Common.Output;
using FluentValidation;

namespace Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceDetail
{
    internal class GetServiceDetailValidator : AbstractValidator<GetServiceDetailQuery>
    {
        public GetServiceDetailValidator()
        {
            RuleFor(s => s.serviceId)
                .NotEmpty().WithMessage(FluentValidationMessageLibrary.NullServiceId)
                .WithErrorCode("400");
        }
    }
}

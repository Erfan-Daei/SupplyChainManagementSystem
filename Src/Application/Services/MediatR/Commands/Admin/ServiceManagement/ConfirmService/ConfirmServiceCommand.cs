using Application.Services.Implement.Commands.Admin.ServiceManagement.ConfirmService;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.ConfirmService
{
    //MediatR command for ConfirmServiceService
    public record ConfirmServiceCommand(Guid serviceId) : IRequest<ResultDto>;

    //MediatR command for ConfirmServiceService
    public class ConfirmServiceCommandHandler : IRequestHandler<ConfirmServiceCommand, ResultDto>
    {
        private readonly IConfirmService _confirmService;
        public ConfirmServiceCommandHandler(IConfirmService confirmService)
        {
            _confirmService = confirmService;
        }
        public async Task<ResultDto> Handle(ConfirmServiceCommand request, CancellationToken cancellationToken)
        {
            return await _confirmService.ConfirmServiceAsync(request, cancellationToken);
        }
    }
}

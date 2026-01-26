using Application.Services.Implement.Commands.Admin.ServiceManagement.DeleteService;
using Common.Output;
using MediatR;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteService
{
    //MediatR command for DeleteServiceService
    public record DeleteServiceCommandRequest(Guid serviceId) : IRequest<DeleteServiceCommand>;
    public record DeleteServiceCommand(DeleteServiceCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto>;

    //MediatR commandHandler for DeleteServiceService

    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, ResultDto>
    {
        private readonly IDeleteService _deleteService;
        public DeleteServiceCommandHandler(IDeleteService deleteService)
        {
            _deleteService = deleteService;
        }
        public async Task<ResultDto> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            return await _deleteService.DeleteServiceAsync(request.commandRequest, cancellationToken);
        }
    }
}

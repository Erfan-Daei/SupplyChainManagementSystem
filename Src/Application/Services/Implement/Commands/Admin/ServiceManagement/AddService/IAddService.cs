using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddService;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.AddService
{
    public interface IAddService
    {
        Task<ResultDto<Guid>> AddServiceAsync(AddServiceCommandRequest request, CancellationToken ct);
    }
}

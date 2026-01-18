using Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteService;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.DeleteService
{
    public interface IDeleteService
    {
        Task<ResultDto> DeleteServiceAsync(DeleteServiceCommandRequest request, CancellationToken ct);
    }
}

using Application.Services.MediatR.Commands.Admin.ServiceManagement.EditService;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.EditService
{
    public interface IEditService
    {
        Task<ResultDto> EditServiceAsync(EditServiceCommandRequest request, CancellationToken ct);
    }
}

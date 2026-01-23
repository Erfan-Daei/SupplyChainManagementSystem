using Application.Services.MediatR.Commands.Admin.ServiceManagement.ConfirmService;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.ConfirmService
{
    public interface IConfirmService
    {
        Task<ResultDto> ConfirmServiceAsync(ConfirmServiceCommand request, CancellationToken ct);
    }
}

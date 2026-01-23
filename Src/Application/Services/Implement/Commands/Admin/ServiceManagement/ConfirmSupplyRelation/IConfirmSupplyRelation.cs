using Application.Services.MediatR.Commands.Admin.ServiceManagement.ConfirmSupplyRelation;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.ConfirmSupplyRelation
{
    public interface IConfirmSupplyRelation
    {
        Task<ResultDto> ConfirmSupplyRelationAsync(ConfirmSupplyRelationCommandRequest request, CancellationToken ct);
    }
}

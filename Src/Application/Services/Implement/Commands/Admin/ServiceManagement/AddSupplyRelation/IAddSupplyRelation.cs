using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddSupplyRelation;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.AddSupplyRelation
{
    public interface IAddSupplyRelation
    {
        Task<ResultDto<Guid>> AddSupplyRelationAsync(AddSupplyRelationCommandRequest request, CancellationToken ct);
    }
}

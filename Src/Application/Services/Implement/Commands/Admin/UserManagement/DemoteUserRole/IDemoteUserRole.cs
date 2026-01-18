using Application.Services.MediatR.Commands.Admin.UserManagement.DemoteUserRole;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.UserManagement.DemoteUserRole
{
    public interface IDemoteUserRole
    {
        Task<ResultDto<Guid>> DemoteUserRoleAsync(DemoteUserRoleCommand request, CancellationToken ct);
    }
}

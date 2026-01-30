using Application.Services.MediatR.Commands.Admin.UserManagement.PromoteUserRole;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.UserManagement.PromoteUserRole
{
    public interface IPromoteUserRole
    {
        Task<ResultDto<Guid>> PromoteUserRoleAsync(PromoteUserRoleCommand request, CancellationToken ct);
    }
}

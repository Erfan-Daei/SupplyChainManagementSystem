using Application.Services.MediatR.Commands.Admin.UserManagement.AssignCompanyToUser;
using Common.Output;

namespace Application.Services.Implement.Commands.Admin.UserManagement.AssignCompanyToUser
{
    public interface IAssignCompanyToUser
    {
        Task<ResultDto> AssignCompanyToUserAsync(AssignCompanyToUserCommandRequest request, CancellationToken ct);
    }
}

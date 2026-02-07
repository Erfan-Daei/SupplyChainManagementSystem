using Application.Services.MediatR.Queries.Users.UserManagement.GetUserList;
using Common.Output;

namespace Application.Services.Implement.Queries.Users.UserManagement.GetUserList
{
    public interface IGetUserList
    {
        Task<ResultDto<List<GetUserListResultDto>>> GetUserListAsync(GetUserListQueryRequest request, CancellationToken ct);
    }
}

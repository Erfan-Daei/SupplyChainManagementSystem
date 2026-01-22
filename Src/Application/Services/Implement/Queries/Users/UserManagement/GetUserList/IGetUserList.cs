using Application.Services.MediatR.Commands.User.UserManagement.GetUserList;
using Common.Output;

namespace Application.Services.Implement.Queries.Users.UserManagement.GetUserList
{
    public interface IGetUserList
    {
        Task<ResultDto<List<GetUserListResultDto>>> GetUserListAsync(GetUserListQueryRequest request, CancellationToken ct);
    }
}

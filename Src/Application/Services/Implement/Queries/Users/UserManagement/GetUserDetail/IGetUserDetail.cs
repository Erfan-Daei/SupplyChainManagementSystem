using Application.Services.MediatR.Commands.User.UserManagement.GetUserDetail;
using Common.Output;

namespace Application.Services.Implement.Queries.Users.UserManagement.GetUserDetail
{
    public interface IGetUserDetail
    {
        Task<ResultDto<GetUserDetailResultDto>> GetUserDetailAsync(GetUserDetailQueryRequest request, CancellationToken ct);
    }
}

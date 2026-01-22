using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Services.MediatR.Commands.User.UserManagement.GetUserList;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Users.UserManagement.GetUserList
{
    public class GetUserListService : IGetUserList
    {
        private readonly IUserRepository_Query _user_Query;
        public GetUserListService(IUserRepository_Query user_Query)
        {
            _user_Query = user_Query;
        }
        public async Task<ResultDto<List<GetUserListResultDto>>> GetUserListAsync(GetUserListQueryRequest request, CancellationToken ct)
        {
            try
            {
                var userList = await _user_Query.GetAllUsersByCompanyId(request.userCompanyId);

                var mappedUserList = userList?.Select(u => new GetUserListResultDto
                {
                    UserId = u.UserId,
                    UserFullName = u.UserFullName,
                    UserEmail = u.UserEmail,
                }).ToList() ?? [];

                return ResultDto<List<GetUserListResultDto>>.Succeeded(mappedUserList, ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<List<GetUserListResultDto>>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

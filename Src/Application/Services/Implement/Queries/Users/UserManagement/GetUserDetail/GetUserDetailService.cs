using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Services.MediatR.Commands.User.UserManagement.GetUserDetail;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Users.UserManagement.GetUserDetail
{
    public class GetUserDetailService : IGetUserDetail
    {
        private readonly IUserRepository_Query _user_Query;   //GetUserDetailByIdAsync
        public GetUserDetailService(IUserRepository_Query user_Query)
        {
            _user_Query = user_Query;
        }
        public async Task<ResultDto<GetUserDetailResultDto>> GetUserDetailAsync(GetUserDetailQueryRequest request, CancellationToken ct)
        {
            try
            {
                var user = await _user_Query.GetUserDetailByIdAsync(request.userId ?? Guid.Empty);
                if (user == null)
                    return ResultDto<GetUserDetailResultDto>.Failed(ResultDtoMessageLibrary.UserNotFound, HttpStatusCode.NotFound);

                return ResultDto<GetUserDetailResultDto>.Succeeded(new GetUserDetailResultDto
                {
                    UserFullName = user.UserFullName,
                    UserEmail = user.UserEmail,
                    UserCompanyName = user.UserCompany.CompanyName,
                    UserCompanyId = user.UserCompanyId,
                    UserRole = user.UserInRole.Role.RoleName,
                    CreatedAt = user.CreatedAt
                }, ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<GetUserDetailResultDto>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

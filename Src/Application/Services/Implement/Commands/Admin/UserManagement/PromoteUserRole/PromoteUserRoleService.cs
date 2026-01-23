using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.JWT;
using Application.Services.MediatR.Commands.Admin.UserManagement.PromoteUserRole;
using Common.Output;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.UserManagement.PromoteUserRole
{
    public class PromoteUserRoleService : IPromoteUserRole
    {
        private readonly IJwtTokenManager _jwtTokenManager;   //GetUserRole
        private readonly IUserRepository_Query _user_Query;   //GetUserWithUserInRoleByUserIdAsync
        public PromoteUserRoleService(IJwtTokenManager jwtTokenManager
            , IUserRepository_Query user_Query)
        {
            _jwtTokenManager = jwtTokenManager;
            _user_Query = user_Query;
        }

        public async Task<ResultDto<Guid>> ChangeUserRoleAsync(PromoteUserRoleCommand request, CancellationToken ct)
        {
            try
            {
                var user = await _user_Query.GetUserWithUserInRoleByUserIdAsync(request.commandRequest.userId);

                var userRole = user!.UserInRole.Role.RoleName;

                var adminRole = _jwtTokenManager.GetUserRole(request.claims);

                if (user!.UserInRole.Role.RoleName.Equals(SeedRoles.CompanyAdminName) && adminRole!.Equals(SeedRoles.CompanyAdminName))
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);

                switch (userRole)
                {
                    case SeedRoles.ViewerName:
                        user.UserInRole.Edit(SeedRoles.CompanyUserId);
                        break;

                    case SeedRoles.CompanyUserName:
                        user.UserInRole.Edit(SeedRoles.CompanyAdminId);
                        break;

                    case SeedRoles.CompanyAdminName:
                        user.UserInRole.Edit(SeedRoles.CompanyAdminId);
                        break;
                }

                return ResultDto<Guid>.Succeeded(user.UserId, ResultDtoMessageLibrary.UserRolePromoted, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<Guid>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

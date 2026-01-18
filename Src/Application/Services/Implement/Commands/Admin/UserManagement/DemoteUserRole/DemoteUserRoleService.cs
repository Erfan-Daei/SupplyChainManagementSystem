using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.JWT;
using Application.Services.MediatR.Commands.Admin.UserManagement.DemoteUserRole;
using Common.Output;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.UserManagement.DemoteUserRole
{
    public class DemoteUserRoleService : IDemoteUserRole
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly IUserRepository_Query _user_Query;
        public DemoteUserRoleService(IJwtTokenManager jwtTokenManager
            , IUserRepository_Query user_Query)
        {
            _jwtTokenManager = jwtTokenManager;
            _user_Query = user_Query;
        }
        public async Task<ResultDto<Guid>> DemoteUserRoleAsync(DemoteUserRoleCommand request, CancellationToken ct)
        {
            try
            {
                var user = await _user_Query.GetUserWithUserInRoleByUserIdAsync(request.commandRequest.userId);

                var userRole = user!.UserInRole.Role.RoleName;

                var adminRole = _jwtTokenManager.GetUserRole(request.adminClaims);

                if (adminRole!.Equals(SeedRoles.CompanyAdminName))
                {
                    if (userRole.Equals(SeedRoles.AdminName) || userRole.Equals(SeedRoles.CompanyAdminName))
                        return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);
                }

                switch (userRole)
                {
                    case SeedRoles.AdminName:
                        UserInRole.Edit(user.UserInRole, SeedRoles.CompanyAdminId);
                        break;

                    case SeedRoles.CompanyAdminName:
                        UserInRole.Edit(user.UserInRole, SeedRoles.CompanyUserId);
                        break;

                    case SeedRoles.CompanyUserName:
                        UserInRole.Edit(user.UserInRole, SeedRoles.ViewerId);
                        break;
                }

                return ResultDto<Guid>.Succeeded(user.UserId, ResultDtoMessageLibrary.UserRoleDemoted, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<Guid>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

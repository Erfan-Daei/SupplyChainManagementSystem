using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
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
        private readonly IJwtTokenManager _jwtTokenManager;   //GetUserRole
        private readonly IUserRepository_Query _user_Query;   //GetUserWithUserInRoleByUserIdAsync
        private readonly IUserRepository_Command _user_Command;   //DeleteUserInRoleAsync   AddUserInRoleAsync   SaveChangesAsync
        private readonly IRoleRepository_Query _role_Query;   //GetRoleByNameAsync
        public DemoteUserRoleService(IJwtTokenManager jwtTokenManager
            , IUserRepository_Query user_Query
            , IUserRepository_Command user_Command
            , IRoleRepository_Query role_Query)
        {
            _jwtTokenManager = jwtTokenManager;
            _user_Query = user_Query;
            _user_Command = user_Command;
            _role_Query = role_Query;
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
                        return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Forbidden);
                }

                UserInRole newUserInRole;
                Role? newRole = new Role();

                switch (userRole)
                {
                    case SeedRoles.AdminName:
                        await _user_Command.DeleteUserInRoleAsync(user.UserInRole);
                        newRole = await _role_Query.GetRoleByNameAsync(SeedRoles.CompanyAdminName);
                        newUserInRole = UserInRole.Create(user.UserId, newRole!.RoleId);
                        await _user_Command.AddUserInRoleAsync(newUserInRole);
                        break;

                    case SeedRoles.CompanyAdminName:
                        await _user_Command.DeleteUserInRoleAsync(user.UserInRole);
                        newRole = await _role_Query.GetRoleByNameAsync(SeedRoles.CompanyUserName);
                        newUserInRole = UserInRole.Create(user.UserId, newRole!.RoleId);
                        await _user_Command.AddUserInRoleAsync(newUserInRole);
                        break;

                    case SeedRoles.CompanyUserName:
                        await _user_Command.DeleteUserInRoleAsync(user.UserInRole);
                        newRole = await _role_Query.GetRoleByNameAsync(SeedRoles.ViewerName);
                        newUserInRole = UserInRole.Create(user.UserId, newRole!.RoleId);
                        await _user_Command.AddUserInRoleAsync(newUserInRole);
                        break;
                }

                await _user_Command.SaveChangesAsync();

                return ResultDto<Guid>.Succeeded(user.UserId, ResultDtoMessageLibrary.UserRoleDemoted(userRole, newRole.RoleName), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<Guid>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

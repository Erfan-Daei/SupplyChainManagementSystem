using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
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
        private readonly IUserRepository_Command _user_Command;   //DeleteUserInRoleAsync   AddUserInRoleAsync   SaveChangesAsync
        private readonly IRoleRepository_Query _role_Query;   //GetRoleByNameAsync
        public PromoteUserRoleService(IJwtTokenManager jwtTokenManager
            , IUserRepository_Query user_Query
            , IUserRepository_Command user_Command
            , IRoleRepository_Query role_Query)
        {
            _jwtTokenManager = jwtTokenManager;
            _user_Query = user_Query;
            _user_Command = user_Command;
            _role_Query = role_Query;
        }

        public async Task<ResultDto<Guid>> PromoteUserRoleAsync(PromoteUserRoleCommand request, CancellationToken ct)
        {
            try
            {
                var user = await _user_Query.GetUserWithUserInRoleByUserIdAsync(request.commandRequest.userId);

                var userRole = user!.UserInRole.Role.RoleName;

                var adminRole = _jwtTokenManager.GetUserRole(request.claims);

                if (user!.UserInRole.Role.RoleName.Equals(SeedRoles.CompanyAdminName) && adminRole!.Equals(SeedRoles.CompanyAdminName))
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.Forbidden, HttpStatusCode.Unauthorized);

                UserInRole newUserInRole;
                Role? newRole = new Role();

                switch (userRole)
                {
                    case SeedRoles.ViewerName:
                        await _user_Command.DeleteUserInRoleAsync(user.UserInRole);
                        newRole = await _role_Query.GetRoleByNameAsync(SeedRoles.CompanyUserName);
                        newUserInRole = UserInRole.Create(user.UserId, newRole!.RoleId);
                        await _user_Command.AddUserInRoleAsync(newUserInRole);
                        break;

                    case SeedRoles.CompanyUserName:
                        await _user_Command.DeleteUserInRoleAsync(user.UserInRole);
                        newRole = await _role_Query.GetRoleByNameAsync(SeedRoles.CompanyAdminName);
                        newUserInRole = UserInRole.Create(user.UserId, newRole!.RoleId);
                        await _user_Command.AddUserInRoleAsync(newUserInRole);
                        break;

                    case SeedRoles.CompanyAdminName:
                        await _user_Command.DeleteUserInRoleAsync(user.UserInRole);
                        newRole = await _role_Query.GetRoleByNameAsync(SeedRoles.AdminName);
                        newUserInRole = UserInRole.Create(user.UserId, newRole!.RoleId);
                        await _user_Command.AddUserInRoleAsync(newUserInRole);
                        break;
                }

                await _user_Command.SaveChangesAsync();

                return ResultDto<Guid>.Succeeded(user.UserId, ResultDtoMessageLibrary.UserRolePromoted(userRole, newRole.RoleName), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<Guid>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

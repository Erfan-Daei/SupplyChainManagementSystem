using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.HashManagement;
using Application.Services.MediatR.Commands.User.UserManagement.SignUp;
using Common.Output;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Users.UserManagement.SignUp
{
    public class SignUpService : ISignUp
    {
        private readonly IUserRepository_Query _user_Query;   //CehckEmailExistAsync
        private readonly ICompanyRepository_Query _company_Query;   //FindCompanyByIdAsync
        private readonly IRoleRepository_Query _role_Query;   //GetRoleByNameAsync
        private readonly IUserRepository_Command _user_Command;   //CreateUser
        private readonly IHashManager _hashManager;   //HashPassword

        public SignUpService
         (
            IUserRepository_Query user_Query,
            ICompanyRepository_Query company_Query,
            IRoleRepository_Query role_Query,
            IUserRepository_Command user_Command,
            IHashManager hashManager
        )
        {
            _user_Query = user_Query;
            _company_Query = company_Query;
            _role_Query = role_Query;
            _user_Command = user_Command;
            _hashManager = hashManager;
        }


        //create User and UserInRole and then give UserId to api for confirmation proccess
        public async Task<ResultDto<Guid>> SignUpAsync(SignUpCommand request, CancellationToken ct)
        {
            try
            {
                //check if email exist
                if (await _user_Query.CheckEmailExistAsync(request.Dto.UserEmail!))
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.AlreadyExistEmail, HttpStatusCode.Conflict);

                //check company is valid
                var comapny = await _company_Query.GetCompanyByIdAsync(request.Dto.CompanyId);
                if (comapny == null)
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.CompanyNotFound, HttpStatusCode.NotFound);

                //check role is valid
                var role = await _role_Query.GetRoleByNameAsync(SeedRoles.ViewerName);
                if (role == null)
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.RoleNotFound, HttpStatusCode.NotFound);

                //hash user password
                var hashedPassword = _hashManager.BCryptHashPassword(request.Dto.Password!);

                var user = User.Create
                (
                    request.Dto.UserFullName!,
                    request.Dto.UserEmail!,
                    hashedPassword,
                    request.Dto.CompanyId
                );

                var userInRole = UserInRole.Create
                (
                    user.UserId,
                    role.RoleId
                );

                user.SetUserInRole(userInRole);

                //create user
                await _user_Command.CreateUserAsync(user, userInRole);

                return ResultDto<Guid>.Succeeded(user.UserId, ResultDtoMessageLibrary.UserCreated, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ResultDto<Guid>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

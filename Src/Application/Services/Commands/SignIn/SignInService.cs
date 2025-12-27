using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.HashManagement;
using Application.Interfaces.Services.Commands.SignIn;
using Application.Validators.Commands;
using Common.Domain_Commons;
using Common.Output;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Commands.SignIn
{
    public class SignInService : ISignIn
    {
        private readonly SignInServiceValidator _validator;   //FluentValidator
        private readonly IUserRepository_Query _user_Query;   //CehckEmailExistAsync
        private readonly ICompanyRepository_Query _company_Query;   //FindCompanyByIdAsync
        private readonly IRoleRepository_Query _role_Query;   //GetRoleByNameAsync
        private readonly IUserRepository_Command _user_Command;   //CreateUser
        private readonly IHashManager _hashManager;   //HashPassword
        public SignInService(SignInServiceValidator validator,
            IUserRepository_Query user_Query,
            IUserRepository_Command user_Command,
            ICompanyRepository_Query company_Query,
            IRoleRepository_Query role_Query,
            IHashManager hashManager)
        {
            _validator = validator;
            _user_Query = user_Query;
            _user_Command = user_Command;
            _company_Query = company_Query;
            _role_Query = role_Query;
            _hashManager = hashManager;
        }

        //create User and UserInRole and then give UserId to api for confirmation proccess
        public async Task<ResultDto<Guid>> CreateUserAsync(SignInServiceRequestDto request)
        {
            var validateResult = _validator.Validate(request);
            if (!validateResult.IsValid)
            {
                return new ResultDto<Guid>()
                {
                    IsSuccess = false,
                    Message = string.Join(" | ", validateResult.Errors.Select(e => e.PropertyName + "=>" + e.ErrorMessage)),
                    StatusCode = HttpStatusCode.BadRequest   // 400
                };
            }
            //check if email exist
            var emailExistResult = await _user_Query.CheckEmailExistAsync(request.UserEmail);
            if (emailExistResult)
            {
                return new ResultDto<Guid>()
                {
                    IsSuccess = false,
                    Message = "ایمیل تکراری است، لطفا یک ایمیل دیگر انتخاب کنید",
                    StatusCode = HttpStatusCode.BadRequest   // 400
                };
            }

            //check company is valid
            var comapny = await _company_Query.FindCompanyByIdAsync(request.CompanyId);
            if (comapny == null)
            {
                return new ResultDto<Guid>()
                {
                    IsSuccess = false,
                    Message = "شرکت مورد نظر یافت نشد، لطفا دوباره تلاش کنید",
                    StatusCode = HttpStatusCode.BadRequest   // 400
                };
            }

            //check role is valid
            var role = await _role_Query.GetRoleByNameAsync(SeedRoles.ViewerName);
            if (role == null)
            {
                return new ResultDto<Guid>()
                {
                    IsSuccess = false,
                    Message = "نقش مورد نظر یافت نشد، لطفا دوباره تلاش کنید",
                    StatusCode = HttpStatusCode.BadRequest   // 400
                };
            }

            //hash user password
            var hashedPassword = _hashManager.HashPassword(request.Password);

            var user = User.CreateUser(request.UserFullName,
                request.UserEmail,
                hashedPassword,
                request.CompanyId);

            var userInRole = UserInRole.CreateUserInRole(user.UserId,
                role.RoleId);

            user.SetUserInRole(userInRole);

            //create user
            var createUserResult = await _user_Command.CreateUserAsync(user, userInRole);
            if (!createUserResult.IsSuccess)
            {
                return new ResultDto<Guid>
                {
                    IsSuccess = false,
                    Message = createUserResult.Message,
                    StatusCode = createUserResult.StatusCode,
                };
            }

            return new ResultDto<Guid>()
            {
                Data = user.UserId,
                IsSuccess = true,
                Message = "حساب کاربری با موفقیت ثبت شد",
                StatusCode = HttpStatusCode.Created,
            };
        }
    }
}

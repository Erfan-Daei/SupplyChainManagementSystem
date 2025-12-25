using Application.Interfaces.Services.Commands.SignIn;
using Common.Domain_Commons;
using Common.Output;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Commands.SignIn
{
    public class SignInService : ISignIn
    {
        private readonly SignInServiceDependency _dependency;   //inject all SignInService Dependencies
        public SignInService(SignInServiceDependency dependency)
        {
            _dependency = dependency;
        }

        //create User and UserInRole and then give UserId to api for confirmation proccess
        public async Task<ResultDto<Guid>> CreateUserAsync(SignInServiceRequestDto request)
        {
            //check if email exist
            var emailExistResult = await _dependency.user_Query.CheckEmailExistAsync(request.UserEmail);
            if (!emailExistResult)
            {
                return new ResultDto<Guid>()
                {
                    IsSuccess = false,
                    Message = "ایمیل تکراری است، لطفا یک ایمیل دیگر انتخاب کنید",
                    StatusCode = HttpStatusCode.BadRequest   // 400
                };
            }

            //check company is valid
            var comapny = await _dependency.company_Query.FindCompanyByIdAsync(request.CompanyId);
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
            var role = await _dependency.role_Query.GetRoleByNameAsync(nameof(SeedRoles.ViewerName));
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
            var hashedPassword = _dependency.hashManager.HashPassword(request.Password);

            var user = new User()
            {
                UserId = Guid.NewGuid(),
                UserFullName = request.UserFullName,
                UserEmail = request.UserEmail,
                UserPassword = hashedPassword,
                UserCompany = comapny,
                UserCompanyId = comapny.CompanyId,
                CreatedAt = DateTime.UtcNow,
            };

            var userInRole = new UserInRole()
            {
                User = user,
                UserId = user.UserId,
                Role = role,
                RoleId = role.RoleId,
                CreatedAt = DateTime.UtcNow
            };
            user.UserInRoles = userInRole;

            //create user
            var createUserResult = await _dependency.user_Command.CreateUserAsync(user, userInRole);
            if (!createUserResult.IsSuccess)
            {
                return new ResultDto<Guid>()
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

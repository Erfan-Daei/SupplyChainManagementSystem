using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Interfaces.HashManagement;
using Application.Interfaces.Services.Commands.ConfirmationEmail;
using Common.Output;
using System.Net;

namespace Application.Services.Commands.ConfirmationEmail.VerifyConfirmationEmail
{
    public class VerifyConfirmationEmail : IVerifyConfirmationEmail
    {
        private readonly IUserRepository_Command _user_Command;   //SaveChangesAsync
        private readonly IUserRepository_Query _user_Query;   //GetUserByIdAsync   GetEmailConfirmationTokenValueAsync
        private readonly IHashManager _hashManager;   //VerifyHashedValue
        public VerifyConfirmationEmail(IUserRepository_Command user_Command,
            IUserRepository_Query user_Query,
            IHashManager hashManager)
        {
            _user_Command = user_Command;
            _user_Query = user_Query;
            _hashManager = hashManager;
        }
        public async Task<ResultDto> VerifyConfirmationEmailAsync(Guid userId, string plainToken)
        {
            try
            {
                if (Guid.Empty == userId || string.IsNullOrEmpty(plainToken))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "لطفا اطاعات رو به درستی وارد کنید",
                        StatusCode = HttpStatusCode.BadRequest   // 404
                    };
                }

                var user = await _user_Query.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد",
                        StatusCode = HttpStatusCode.NotFound   // 404
                    };
                }

                var userToken = await _user_Query.GetEmailConfirmationTokenValueAsync(userId);

                if (userToken == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "توکن کاربر منقضی شده یا وجود ندارد",
                        StatusCode = HttpStatusCode.NotFound   // 404
                    };
                }
                if (userToken.CheckIsExpired())
                {
                    await _user_Command.DeleteUserTokenAsync(userToken);
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "توکن کاربر منقضی شده",
                        StatusCode = HttpStatusCode.Unauthorized   // 401
                    };
                }

                var verifyTokenResult = _hashManager.VerifyHashedValue(plainToken, userToken.UserTokenValue);

                if (!verifyTokenResult)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "توکن ورودی اشتباه است",
                        StatusCode = HttpStatusCode.BadRequest  // 400
                    };
                }

                userToken.SetIsUsed();
                user.SetDeletedAt();   //soft delete userToken after being used
                user.ChangeUserEmailConfirmedState();

                await _user_Command.SaveChangesAsync();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "ایمیل شما با موفقیت تایید شد",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}

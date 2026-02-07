using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Services.MediatR.Commands.User.UserManagement.DeleteUser;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Commands.Users.UserManagement.DeleteUser
{
    public class DeleteUserService : IDeleteUser
    {
        private readonly IUserRepository_Query _user_Query;   //GetUserByIdAsync
        private readonly IUserRepository_Command _user_Command;   //SaveChangesAsync
        public DeleteUserService(IUserRepository_Query user_Query
            , IUserRepository_Command user_Command)
        {
            _user_Query = user_Query;
            _user_Command = user_Command;
        }

        public async Task<ResultDto> DeleteUserAsync(DeleteUserCommandRequest request, CancellationToken ct)
        {
            try
            {
                var user = await _user_Query.GetUserByIdAsync(request.userId ?? Guid.Empty);
                if (user == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.UserNotFound, HttpStatusCode.NotFound);

                user.SetDeletedAt();

                await _user_Command.SaveChangesAsync();

                return ResultDto.Succeeded(ResultDtoMessageLibrary.UserDeleted, HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

using Application.Services.MediatR.Commands.User.UserManagement.DeleteUser;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.UserManagement.DeleteUser
{
    public interface IDeleteUser
    {
        Task<ResultDto> DeleteUserAsync(DeleteUserCommandRequest request, CancellationToken ct);
    }
}

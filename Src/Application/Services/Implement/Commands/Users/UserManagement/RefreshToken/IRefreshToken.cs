using Application.Services.MediatR.Commands.User.UserManagement.RefreshToken;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.UserManagement.RefreshToken
{
    public interface IRefreshToken
    {
        Task<ResultDto<RefreshTokenServiceResultDto>> GenerateRefreshTokenAsync(RefreshTokenCommand request, CancellationToken ct);
    }
}

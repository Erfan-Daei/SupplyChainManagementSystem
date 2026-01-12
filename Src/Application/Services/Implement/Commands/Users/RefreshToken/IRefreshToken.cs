using Application.Services.MediatR.Commands.User.RefreshToken;
using Common.Output;

namespace Application.Services.Implement.Commands.Users.RefreshToken
{
    public interface IRefreshToken
    {
        Task<ResultDto<RefreshTokenServiceResultDto>> GenerateRefreshTokenAsync(RefreshTokenCommand request, CancellationToken ct);
    }
}

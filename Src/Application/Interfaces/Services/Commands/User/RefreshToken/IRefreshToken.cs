using Application.Dtos.Services.Commands.User.RefreshToken;
using Application.MediatR.Services.Commands.User.RefreshToken;
using Common.Output;

namespace Application.Interfaces.Services.Commands.User.RefreshToken
{
    public interface IRefreshToken
    {
        Task<ResultDto<RefreshTokenServiceResultDto>> GenerateRefreshTokenAsync(RefreshTokenCommand request, CancellationToken ct);
    }
}

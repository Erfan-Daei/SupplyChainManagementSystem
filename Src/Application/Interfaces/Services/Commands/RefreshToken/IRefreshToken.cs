using Application.Dtos.Services.Commands.RefreshToken;
using Application.MediatR.Services.Commands.RefreshToken;
using Common.Output;

namespace Application.Interfaces.Services.Commands.RefreshToken
{
    public interface IRefreshToken
    {
        Task<ResultDto<RefreshTokenServiceResultDto>> GenerateRefreshTokenAsync(RefreshTokenCommand request, CancellationToken ct);
    }
}

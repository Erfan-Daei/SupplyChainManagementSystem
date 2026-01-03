using Application.Dtos.Services.Commands.RefreshToken;
using Application.Interfaces.Services.Commands.RefreshToken;
using Application.MediatR.Services.Commands.RefreshToken;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Commands.RefreshToken
{
    //command handler for MediatR refreshToken Service
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ResultDto<RefreshTokenServiceResultDto>>
    {
        private readonly IRefreshToken _refreshToken;
        public RefreshTokenCommandHandler(IRefreshToken refreshToken)
        {
            _refreshToken = refreshToken;
        }

        public async Task<ResultDto<RefreshTokenServiceResultDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _refreshToken.GenerateRefreshTokenAsync(request, cancellationToken);
        }
    }
}

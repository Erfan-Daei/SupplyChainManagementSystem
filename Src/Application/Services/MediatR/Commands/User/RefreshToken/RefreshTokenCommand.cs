using Application.Services.Implement.Commands.Users.RefreshToken;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Commands.User.RefreshToken
{
    //Command for MediatR RefreshToken Service
    public record RefreshTokenCommand(string refreshToken) : IRequest<ResultDto<RefreshTokenServiceResultDto>>;

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

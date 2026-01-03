using Application.Dtos.Services.Commands.RefreshToken;
using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.RefreshToken
{
    //Command for MediatR RefreshToken Service
    public record RefreshTokenCommand(string refreshToken) : IRequest<ResultDto<RefreshTokenServiceResultDto>>;
}

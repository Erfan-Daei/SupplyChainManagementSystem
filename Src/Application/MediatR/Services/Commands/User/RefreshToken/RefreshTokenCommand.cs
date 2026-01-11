using Application.Dtos.Services.Commands.User.RefreshToken;
using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Commands.User.RefreshToken
{
    //Command for MediatR RefreshToken Service
    public record RefreshTokenCommand(string refreshToken) : IRequest<ResultDto<RefreshTokenServiceResultDto>>;
}

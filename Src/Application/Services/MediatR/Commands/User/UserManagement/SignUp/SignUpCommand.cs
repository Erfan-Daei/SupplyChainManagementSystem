using Application.Interfaces.Auth;
using Application.Services.Implement.Commands.Users.UserManagement.SignUp;
using Common.Output;
using Domain.Entities.Common;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.User.UserManagement.SignUp
{
    //MediatR Command for SignInService
    public record SignUpCommandRequest(SignUpServiceRequestDto Dto) : IRequest<SignUpCommand>;
    public record SignUpCommand(SignUpCommandRequest commandRequest, ClaimsPrincipal claimsPrincipal) : IRequest<ResultDto<Guid>>;

    //MediatR Handler for SignInServiceCommand
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, ResultDto<Guid>>
    {
        private readonly IAuthManager _authManager;
        private readonly ISignUp _signIn;
        public SignUpCommandHandler(IAuthManager authManager
            , ISignUp signIn)
        {
            _authManager = authManager;
            _signIn = signIn;
        }

        public async Task<ResultDto<Guid>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            if (request.claimsPrincipal.Identities.Any(i => i.IsAuthenticated == true))
            {
                var checkAccess = await _authManager.CheckAccessToCompany(request.claimsPrincipal.Claims, request.commandRequest.Dto.CompanyId ?? SeedCompanies.DefaultCompanyId);
                if (!checkAccess)
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);
            }
            return await _signIn.SignUpAsync(request.commandRequest, cancellationToken);
        }
    }
}

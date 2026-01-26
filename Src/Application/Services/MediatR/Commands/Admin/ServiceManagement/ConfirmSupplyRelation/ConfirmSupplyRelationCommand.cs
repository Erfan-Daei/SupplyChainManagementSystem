using Application.Interfaces.Auth;
using Application.Services.Implement.Commands.Admin.ServiceManagement.ConfirmSupplyRelation;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.ConfirmSupplyRelation
{
    //MediatR command ConfirmSupplyRelationService
    public record ConfirmSupplyRelationCommandRequest(Guid supplyRelationId) : IRequest<ConfirmSupplyRelationCommand>;
    public record ConfirmSupplyRelationCommand(ConfirmSupplyRelationCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto>;

    //MediatR commandHandler ConfirmSupplyRelationService
    public class ConfirmSupplyRelationCommandHandler : IRequestHandler<ConfirmSupplyRelationCommand, ResultDto>
    {
        private readonly IAuthManager _authManager;
        private readonly IConfirmSupplyRelation _confirmSupplyRelation;
        public ConfirmSupplyRelationCommandHandler(IAuthManager authManager
            , IConfirmSupplyRelation confirmSupplyRelation)
        {
            _authManager = authManager;
            _confirmSupplyRelation = confirmSupplyRelation;
        }
        public async Task<ResultDto> Handle(ConfirmSupplyRelationCommand request, CancellationToken cancellationToken)
        {
            var checkAccess = await _authManager.CheckAccessToConfirmSupplyRelation(request.adminClaims, request.commandRequest.supplyRelationId);
            if (!checkAccess)
                return ResultDto.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);

            return await _confirmSupplyRelation.ConfirmSupplyRelationAsync(request.commandRequest, cancellationToken);
        }
    }
}

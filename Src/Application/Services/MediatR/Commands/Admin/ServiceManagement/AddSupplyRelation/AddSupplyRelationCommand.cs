using Application.Interfaces.Auth;
using Application.Services.Implement.Commands.Admin.ServiceManagement.AddSupplyRelation;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.AddSupplyRelation
{
    //MediatR command for AddSupplyRelationService
    public record AddSupplyRelationCommandRequest(Guid serviceId, Guid supplierCompanyId, Guid consumerCompanyId) : IRequest<AddSupplyRelationCommand>;
    public record AddSupplyRelationCommand(AddSupplyRelationCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto<Guid>>;

    //MediatR commandHandler for AddSupplyRelationService
    public class AddSupplyRelationCommandHandler : IRequestHandler<AddSupplyRelationCommand, ResultDto<Guid>>
    {
        private readonly IAuthManager _authManager;
        private readonly IAddSupplyRelation _addSupplyRelation;
        public AddSupplyRelationCommandHandler(IAuthManager authManager
            , IAddSupplyRelation addSupplyRelation)
        {
            _authManager = authManager;
            _addSupplyRelation = addSupplyRelation;
        }
        public async Task<ResultDto<Guid>> Handle(AddSupplyRelationCommand request, CancellationToken cancellationToken)
        {
            var checkAccess = await _authManager.CheckAccessToAddSupplyRelation(request.adminClaims, request.commandRequest.consumerCompanyId);
            if (!checkAccess)
                return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);

            return await _addSupplyRelation.AddSupplyRelationAsync(request.commandRequest, cancellationToken);
        }
    }
}

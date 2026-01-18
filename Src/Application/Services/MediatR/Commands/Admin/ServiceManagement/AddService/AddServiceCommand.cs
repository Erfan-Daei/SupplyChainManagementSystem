using Application.Interfaces.Authorization;
using Application.Services.Implement.Commands.Admin.ServiceManagement.AddService;
using Common.Output;
using MediatR;
using System.Net;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.AddService
{
    //MediatR command for AddServiceService
    public record AddServiceCommandRequest(Guid companyId, string serviceName, string serviceDescription) : IRequest<AddServiceCommand>;
    public record AddServiceCommand(AddServiceCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto<Guid>>;

    //MediatR commandHandler for AddServiceService
    public class AddServiceCommandHandler : IRequestHandler<AddServiceCommand, ResultDto<Guid>>
    {
        private readonly IAuthManager _authManager;
        private readonly IAddService _addService;
        public AddServiceCommandHandler(IAuthManager authManager
            , IAddService addService)
        {
            _authManager = authManager;
            _addService = addService;
        }
        public async Task<ResultDto<Guid>> Handle(AddServiceCommand request, CancellationToken cancellationToken)
        {
            /*var checkAccess = await _authManager.ChechAccessToCompany(request.adminClaims, request.commandRequest.companyId);
            if (!checkAccess)
                return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);*/

            return await _addService.AddServiceAsync(request.commandRequest, cancellationToken);
        }
    }
}

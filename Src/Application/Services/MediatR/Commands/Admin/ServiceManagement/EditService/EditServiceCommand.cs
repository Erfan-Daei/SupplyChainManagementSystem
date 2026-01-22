using Application.Interfaces.Auth;
using Application.Services.Implement.Commands.Admin.ServiceManagement.EditService;
using Common.Output;
using MediatR;
using System.Security.Claims;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.EditService
{
    //MediatR command for EditServiceService
    public record EditServiceCommandRequest(EditServiceRequestDto Dto) : IRequest<EditServiceCommand>;
    public record EditServiceCommand(EditServiceCommandRequest commandRequest, IEnumerable<Claim> adminClaims) : IRequest<ResultDto>;

    //MediatR commandHandler for EditServiceService
    public class EditServiceCommandHandler : IRequestHandler<EditServiceCommand, ResultDto>
    {
        private readonly IAuthManager _authManager;
        private readonly IEditService _editService;
        public EditServiceCommandHandler(IAuthManager authManager
            , IEditService editService)
        {
            _authManager = authManager;
            _editService = editService;
        }
        public async Task<ResultDto> Handle(EditServiceCommand request, CancellationToken cancellationToken)
        {
            /*var checkAccess = await _authManager.ChechAccessToService(request.adminClaims, request.commandRequest.Dto.ServiceId);
            if (!checkAccess)
                return ResultDto.Failed(ResultDtoMessageLibrary.UnAuthorized, HttpStatusCode.Unauthorized);*/

            return await _editService.EditServiceAsync(request.commandRequest, cancellationToken);
        }
    }
}

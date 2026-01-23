using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.ConfirmService;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.ConfirmService
{
    public class ConfirmServiceService : IConfirmService
    {
        private readonly IServiceRepository_Query _service_Query;   //GetServiceByIdAsync
        private readonly IServiceRepository_Command _command_Command;   //SaveChangesAsync
        public ConfirmServiceService(IServiceRepository_Query service_Query
            , IServiceRepository_Command service_Command)
        {
            _service_Query = service_Query;
            _command_Command = service_Command;
        }
        public async Task<ResultDto> ConfirmServiceAsync(ConfirmServiceCommand request, CancellationToken ct)
        {
            try
            {
                var service = await _service_Query.GetServiceByIdAsync(request.serviceId);
                if (service == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.ServiceNotFound, HttpStatusCode.NotFound);

                service.ConfirmService();

                await _command_Command.SaveChangesAsync();

                return ResultDto.Succeeded(ResultDtoMessageLibrary.ServiceConfirmed, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

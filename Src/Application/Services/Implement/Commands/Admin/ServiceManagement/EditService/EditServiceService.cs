using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.EditService;
using Common.Output;
using Domain.Entities.ServiceManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.EditService
{
    public class EditServiceService : IEditService
    {
        private readonly IServiceRepository_Query _service_Query;   //GetServiceByIdAsync
        private readonly IServiceRepository_Command _service_Command;   //SaveChangesAsync
        public EditServiceService(IServiceRepository_Query service_Query
            , IServiceRepository_Command service_Command)
        {
            _service_Query = service_Query;
            _service_Command = service_Command;
        }
        public async Task<ResultDto> EditServiceAsync(EditServiceCommandRequest request, CancellationToken ct)
        {
            try
            {
                var service = await _service_Query.GetServiceByIdAsync(request.Dto.ServiceId);
                if (service == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.ServiceNotFound, HttpStatusCode.NotFound);

                Service.Edit(service, request.Dto.ServiceName, request.Dto.ServiceDescription);

                await _service_Command.SaveChangesAsync();

                return ResultDto.Succeeded(ResultDtoMessageLibrary.ServiceEdited, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddService;
using Common.Output;
using Domain.Entities.ServiceManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.AddService
{
    public class AddServiceService : IAddService
    {
        private readonly IServiceRepository_Command _service_Command;
        public AddServiceService(IServiceRepository_Command service_Command)
        {
            _service_Command = service_Command;
        }
        public async Task<ResultDto<Guid>> AddServiceAsync(AddServiceCommandRequest request, CancellationToken ct)
        {
            try
            {
                var service = Service.Create(request.serviceName, request.serviceDescription, request.companyId);

                await _service_Command.AddServiceAsync(service);

                return ResultDto<Guid>.Succeeded(service.ServiceId, ResultDtoMessageLibrary.ServiceCreated, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ResultDto<Guid>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

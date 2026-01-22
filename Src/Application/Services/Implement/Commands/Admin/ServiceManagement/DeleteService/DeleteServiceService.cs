using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteService;
using Common.Output;
using Domain.Entities.ServiceManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.DeleteService
{
    public class DeleteServiceService : IDeleteService
    {
        private readonly IServiceRepository_Query _service_Query;   //GetServiceByIdAsync   GetAllSupplierCompanyByServiceIdAsync   GetAllSupplyRelationByServiceIdAsync
        private readonly IServiceRepository_Command _service_Command;   //SaveChangesAsync
        public DeleteServiceService(IServiceRepository_Query service_Query
            , IServiceRepository_Command service_Command)
        {
            _service_Query = service_Query;
            _service_Command = service_Command;
        }

        public async Task<ResultDto> DeleteServiceAsync(DeleteServiceCommandRequest request, CancellationToken ct)
        {
            try
            {
                var service = await _service_Query.GetServiceByIdAsync(request.serviceId);
                if (service == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.ServiceNotFound, HttpStatusCode.NotFound);

                service.SetDeletedAt();

                //remove this Service from SupplierCompany list
                var supplierCompanies = await _service_Query.GetAllSupplierCompanyByServiceIdAsync(service.ServiceId);
                if (supplierCompanies != null)
                {
                    foreach (var company in supplierCompanies)
                    {
                        Company.RemoveService(company, service);
                    }
                }

                //DeActive All SupplyRelations
                var supplyRelations = await _service_Query.GetAllSupplyRelationByServiceIdAsync(service.ServiceId);
                if (supplyRelations != null)
                {
                    foreach (var relation in supplyRelations)
                    {
                        relation.DeActiveSupplyRelation();
                    }
                }

                await _service_Command.SaveChangesAsync();

                return ResultDto.Succeeded(ResultDtoMessageLibrary.ServiceDeleted, HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

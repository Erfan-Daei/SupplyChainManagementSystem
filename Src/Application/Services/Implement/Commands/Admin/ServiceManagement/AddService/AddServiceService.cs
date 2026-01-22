using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddService;
using Common.Output;
using Domain.Entities.ServiceManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.AddService
{
    public class AddServiceService : IAddService
    {
        private readonly IServiceRepository_Command _service_Command;   //AddServiceAsync
        private readonly ICompanyRepository_Query _company_Query;   //GetCompanyByIdAsync
        public AddServiceService(IServiceRepository_Command service_Command
            , ICompanyRepository_Query company_Query)
        {
            _service_Command = service_Command;
            _company_Query = company_Query;
        }
        public async Task<ResultDto<Guid>> AddServiceAsync(AddServiceCommandRequest request, CancellationToken ct)
        {
            try
            {
                var supplierCompany = await _company_Query.GetCompanyByIdAsync(request.companyId);
                if (supplierCompany == null)
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.CompanyNotFound, HttpStatusCode.NotFound);

                var service = Service.Create(request.serviceName, request.serviceDescription, request.companyId);

                //CreatorCompany is first SupplierCompany too
                Service.AddSupplierCompany(service, supplierCompany);

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

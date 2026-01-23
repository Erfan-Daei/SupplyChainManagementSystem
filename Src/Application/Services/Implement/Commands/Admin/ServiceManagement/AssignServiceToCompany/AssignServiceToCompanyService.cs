using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.AssignServiceToCompany;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.AssignServiceToCompany
{
    public class AssignServiceToCompanyService : IAssignServiceToCompany
    {
        private readonly ICompanyRepository_Query _company_Query;   //GetCompanyByIdAsync
        private readonly ICompanyRepository_Command _company_Command;   //SaveChangesAsync
        private readonly IServiceRepository_Query _service_Query;   //GetServiceByIdAsync
        public AssignServiceToCompanyService(ICompanyRepository_Query company_Query
            , ICompanyRepository_Command company_Command
            , IServiceRepository_Query service_Query)
        {
            _company_Query = company_Query;
            _company_Command = company_Command;
            _service_Query = service_Query;
        }
        public async Task<ResultDto> AssignServiceToCompanyAsync(AssignServiceToCompanyCommandRequest request, CancellationToken ct)
        {
            try
            {
                var company = await _company_Query.GetCompanyByIdAsync(request.companyId);
                if (company == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.CompanyNotFound, HttpStatusCode.NotFound);

                var service = await _service_Query.GetServiceByIdAsync(request.serviceId);
                if (service == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.ServiceNotFound, HttpStatusCode.NotFound);

                company.AddService(service);

                await _company_Command.SaveChangesAsync();

                return ResultDto.Succeeded(ResultDtoMessageLibrary.ServiceAssignedToCompany, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

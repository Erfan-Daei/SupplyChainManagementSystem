using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.EditCompany;
using Common.Output;
using Domain.Entities.ServiceManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.EditCompany
{
    public class EditCompanyService : IEditCompany
    {
        private readonly ICompanyRepository_Command _company_Command;   //SaveChangesAsync
        private readonly ICompanyRepository_Query _company_Query;   //FindCompanyByIdAsync
        public EditCompanyService(ICompanyRepository_Command company_Command
            , ICompanyRepository_Query company_Query)
        {
            _company_Command = company_Command;
            _company_Query = company_Query;
        }

        public async Task<ResultDto> EditCompanyAsync(EditCompanyCommandRequest request, CancellationToken ct)
        {
            try
            {
                var company = await _company_Query.GetCompanyByIdAsync(request.companyId);
                if (company == null)
                    ResultDto.Failed(ResultDtoMessageLibrary.CompanyNotFound, HttpStatusCode.NotFound);

                company.Edit(request.companyName);

                await _company_Command.SaveChangesAsync();

                return ResultDto.Succeeded(ResultDtoMessageLibrary.CompanyEdited, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

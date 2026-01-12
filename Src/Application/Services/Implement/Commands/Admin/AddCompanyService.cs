using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Services.MediatR.Commands.Admin.AddCompany;
using Common.Output;
using Domain.Entities.ServiceManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin
{
    public class AddCompanyService : IAddCompany
    {
        private readonly ICompanyRepository_Command _company_Command;   //AddCompanyAsync
        public AddCompanyService(ICompanyRepository_Command company_Command)
        {
            _company_Command = company_Command;
        }

        public async Task<ResultDto<Guid>> AddCompanyAsync(AddCompanyCommand request, CancellationToken ct)
        {
            try
            {
                var company = Company.Create(request.companyName);

                await _company_Command.AddCompanyAsync(company);

                return ResultDto<Guid>.Succeeded(company.CompanyId, ResultDtoMessageLibrary.CompanyCreated, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ResultDto<Guid>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

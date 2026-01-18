using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.UserManagementRepository;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteCompany;
using Common.Output;
using Domain.Entities.Common;
using Domain.Entities.UserManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.DeleteCompany
{
    public class DeleteCompanyService : IDeleteCompany
    {
        private readonly ICompanyRepository_Query _company_Query;
        private readonly ICompanyRepository_Command _company_Command;
        private readonly IUserRepository_Query _user_Query;
        public DeleteCompanyService(ICompanyRepository_Query company_Query
            , ICompanyRepository_Command company_Command
            , IUserRepository_Query user_Query)
        {
            _company_Query = company_Query;
            _company_Command = company_Command;
            _user_Query = user_Query;
        }
        public async Task<ResultDto> DeleteCompanyAsync(DeleteCompanyCommand request, CancellationToken ct)
        {
            try
            {
                var company = await _company_Query.GetCompanyByIdAsync(request.companyId);
                if (company == null)
                    return ResultDto.Failed(ResultDtoMessageLibrary.CompanyNotFound, HttpStatusCode.NotFound);

                company.SetDeletedAt();

                //demote all usersRoles and assign them to defaultCompany
                var allCompanyUsers = await _user_Query.GetAllUsersByCompanyId(request.companyId);

                if (allCompanyUsers != null)
                {
                    foreach (var user in allCompanyUsers)
                    {
                        UserInRole.Edit(user.UserInRole, SeedRoles.ViewerId);
                        User.AssignCompany(user, SeedCompanies.DefaultCompanyId);
                    }
                }

                await _company_Command.SaveChangesAsync();

                return ResultDto.Succeeded(ResultDtoMessageLibrary.CompanyDeleted, HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return ResultDto.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

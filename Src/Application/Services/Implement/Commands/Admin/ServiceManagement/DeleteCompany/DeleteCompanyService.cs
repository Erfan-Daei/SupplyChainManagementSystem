using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Commands.UserManagementRepository;
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
        private readonly ICompanyRepository_Query _company_Query;   //GetCompanyByIdAsync   GetServiceListFromSupplierIdAsync
        private readonly ICompanyRepository_Command _company_Command;   //SaveChangesAsync
        private readonly IUserRepository_Query _user_Query;   //GetAllUsersByCompanyId
        private readonly ISupplyRelationRepository_Query _supply_RelationQuery;   //GetAllCompanySupplyRelation
        private readonly IUserRepository_Command _user_Command;   //DeleteUserInRoleAsync   AddUserInRoleAsync
        private readonly IRoleRepository_Query _role_Query;   //GetRoleByNameAsync
        public DeleteCompanyService(ICompanyRepository_Query company_Query
            , ICompanyRepository_Command company_Command
            , IUserRepository_Query user_Query
            , ISupplyRelationRepository_Query supply_RelationQuery
            , IUserRepository_Command user_Command
            , IRoleRepository_Query role_Query)
        {
            _company_Query = company_Query;
            _company_Command = company_Command;
            _user_Query = user_Query;
            _supply_RelationQuery = supply_RelationQuery;
            _user_Command = user_Command;
            _role_Query = role_Query;
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
                var defaultCompany = await _company_Query.GetCompanyByIdAsync(SeedCompanies.DefaultCompanyId);
                var allCompanyUsers = await _user_Query.GetAllUsersByCompanyId(request.companyId);

                if (allCompanyUsers != null)
                {
                    allCompanyUsers.RemoveAll(u => u.UserInRole.RoleId == SeedRoles.AdminId);

                    foreach (var user in allCompanyUsers)
                    {
                        await _user_Command.DeleteUserInRoleAsync(user.UserInRole);
                        var newRole = await _role_Query.GetRoleByNameAsync(SeedRoles.CompanyUserName);
                        var newUserInRole = UserInRole.Create(user.UserId, newRole!.RoleId);
                        await _user_Command.AddUserInRoleAsync(newUserInRole);
                        user.AssignCompany(defaultCompany!);
                    }
                }

                //remove from Services SupplierCompany
                var companyServices = await _company_Query.GetServiceListFromSupplierIdAsync(request.companyId);

                if (companyServices != null)
                {
                    foreach (var service in companyServices)
                    {
                        service.RemoveSupplierCompany(company);
                    }
                }

                //inActive All Supplyrelations
                var companySupplyRelations = await _supply_RelationQuery.GetAllCompanySupplyRelationAsync(company.CompanyId);
                if (companySupplyRelations != null)
                {
                    foreach (var relation in companySupplyRelations)
                    {
                        relation.DeActiveSupplyRelation();
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

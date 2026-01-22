using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository
{
    //interface for all Company Query services
    public interface ICompanyRepository_Query
    {
        Task<Company?> GetCompanyByIdAsync(Guid companyId);
        Task<List<Company>?> GetCompanyListAsync();   //Get All Companies
        Task<Company?> GetCompanyDetailAsync(Guid companyId);
        Task<List<Service>?> GetServiceListFromSupplierIdAsync(Guid companyId);
    }
}

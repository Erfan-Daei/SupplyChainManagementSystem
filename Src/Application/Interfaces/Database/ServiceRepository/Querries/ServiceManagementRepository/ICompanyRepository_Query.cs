using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository
{
    public interface ICompanyRepository_Query   //interface centeralize all Company table Query (Select)  methods
    {
        Task<Company?> GetCompanyByIdAsync(Guid companyId);
        Task<List<Company>?> GetCompanyListAsync();
        Task<Company?> GetCompanyDetailAsync(Guid companyId);
    }
}

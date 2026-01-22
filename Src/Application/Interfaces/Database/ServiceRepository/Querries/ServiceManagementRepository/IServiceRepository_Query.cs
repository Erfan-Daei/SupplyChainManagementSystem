using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository
{
    //interface for all Service Query services
    public interface IServiceRepository_Query
    {
        Task<Service?> GetServiceByIdAsync(Guid serviceId);
        Task<List<Service>?> GetServiceListAsync();
        Task<Guid> GetServiceCreatorCompanyIdById(Guid serviceId);
        Task<List<Service>?> GetServiceListFromSupplierIdAsync(Guid creatorCompanyId);
    }
}

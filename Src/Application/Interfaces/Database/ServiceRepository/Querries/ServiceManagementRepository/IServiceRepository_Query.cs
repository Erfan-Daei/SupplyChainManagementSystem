using Domain.Entities.ServiceManagement;

namespace Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository
{
    //interface for all Service Query services
    public interface IServiceRepository_Query
    {
        Task<Service?> GetServiceByIdAsync(Guid serviceId);
        Task<List<Service>?> GetServiceListAsync();   //get all Services
        Task<Guid> GetServiceCreatorCompanyIdByIdAsync(Guid serviceId);
        Task<List<Company>?> GetAllSupplierCompanyByServiceIdAsync(Guid serviceId);   //get all Companies which Supply this Service
        Task<List<SupplyRelation>?> GetAllSupplyRelationByServiceIdAsync(Guid serviceId);   //get all SupplyRelation which contains ServiceId
        Task<Service?> GetServiceDetailAsync(Guid serviceId);   //get Service with all SupplierCompanies and SupplyRelations
    }
}

using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Domain.Entities.ServiceManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Persistence.DatabaseManagement.ExceptionHandler.DatabaseExceptionHandler;

namespace Persistence.ServiceRepository.Queries.ServiceManagementRepository
{
    public class ServiceRepository_Query : IServiceRepository_Query
    {
        private readonly IDatabaseContext _databaseContext;
        public ServiceRepository_Query(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Service?> GetServiceByIdAsync(Guid serviceId)
        {
            try
            {
                return await _databaseContext.Services.FirstOrDefaultAsync(s => s.ServiceId == serviceId);
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<List<Service>?> GetServiceListAsync()
        {
            try
            {
                return await _databaseContext.Services.ToListAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<Guid> GetServiceCreatorCompanyIdByIdAsync(Guid serviceId)
        {
            try
            {
                var service = await _databaseContext.Services.FirstOrDefaultAsync(s => s.ServiceId == serviceId);
                return service?.CreatorCompanyId ?? Guid.Empty;
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return Guid.Empty;
            }
        }

        public async Task<List<Company>?> GetAllSupplierCompanyByServiceIdAsync(Guid serviceId)
        {
            try
            {
                return await _databaseContext.Services.Where(s => s.ServiceId.Equals(serviceId))
                    .Include(s => s.SupplierCompanies)
                    .Select(s => s.SupplierCompanies.ToList())
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<List<SupplyRelation>?> GetAllSupplyRelationByServiceIdAsync(Guid serviceId)
        {
            try
            {
                return await _databaseContext.Services.Where(s => s.ServiceId.Equals(serviceId))
                    .Include(s => s.SupplyRelations)
                    .Select (s => s.SupplyRelations.ToList())
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<Service?> GetServiceDetailAsync(Guid serviceId)
        {
            try
            {
                return await _databaseContext.Services.Where(s => s.ServiceId.Equals(serviceId))
                    .Include(s => s.SupplyRelations)
                    .Include(s => s.SupplierCompanies)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }
    }
}

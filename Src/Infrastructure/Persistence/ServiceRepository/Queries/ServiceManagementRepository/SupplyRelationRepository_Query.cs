using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Domain.Entities.ServiceManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Persistence.DatabaseManagement.ExceptionHandler.DatabaseExceptionHandler;

namespace Persistence.ServiceRepository.Queries.ServiceManagementRepository
{
    public class SupplyRelationRepository_Query : ISupplyRelationRepository_Query
    {
        private readonly IDatabaseContext _databaseContext;
        public SupplyRelationRepository_Query(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Guid>?> GetNextRelationsAsync(Guid consumerCompanyId)
        {
            try
            {
                return await _databaseContext.SupplyRelations.Where(sr => sr.SupplierCompanyId == consumerCompanyId)
                    .Select(sr => sr.ConsumerCompanyId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<SupplyRelation?> GetSupplyRelationByIdAsync(Guid supplyRelationId)
        {
            try
            {
                return await _databaseContext.SupplyRelations.FirstOrDefaultAsync(sr => sr.SupplyRelationId == supplyRelationId);
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<SupplyRelation?> GetSupplyRelationDetailByIdAsync(Guid supplyRelationId)
        {
            try
            {
                return await _databaseContext.SupplyRelations.Where(sr => sr.SupplyRelationId == supplyRelationId)
                    .Include(sr => sr.Service)
                    .Include(sr => sr.SupplierCompany)
                    .Include(sr => sr.ConsumerCompany)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<List<SupplyRelation>?> GetAllCompanySupplyRelationAsync(Guid companyId)
        {
            try
            {
                return await _databaseContext.SupplyRelations
                    .Where(sr => sr.SupplierCompanyId == companyId || sr.ConsumerCompanyId == companyId)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<List<SupplyRelation>?> GetAllSupplyRelationBySupplierIdAsync(Guid supplierCompanyId)
        {
            try
            {
                return await _databaseContext.SupplyRelations
                    .Where(sr => sr.SupplierCompanyId == supplierCompanyId)
                    .Include(sr => sr.ConsumerCompany)
                    .Include(sr => sr.Service)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<List<SupplyRelation>?> GetAllSupplyRelationByConsumerIdAsync(Guid consumerCompanyId)
        {
            try
            {
                return await _databaseContext.SupplyRelations
                    .Where(sr => sr.ConsumerCompanyId == consumerCompanyId)
                    .Include(sr => sr.SupplierCompany)
                    .Include(sr => sr.Service)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<List<SupplyRelation>?> GetAllUnConfirmedSupplyRelationByCompanyIdAsync(Guid companyId)
        {
            try
            {
                return await _databaseContext.SupplyRelations
                    .Where(sr => (!sr.SupplyRelationIsConfirmed) && (sr.SupplierCompanyId == companyId || sr.ConsumerCompanyId == companyId))
                    .Include(sr => sr.SupplierCompany)
                    .Include(sr => sr.ConsumerCompany)
                    .Include(sr => sr.Service)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }
    }
}

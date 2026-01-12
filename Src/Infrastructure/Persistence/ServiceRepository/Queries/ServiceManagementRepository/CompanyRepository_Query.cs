using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Domain.Entities.ServiceManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Persistence.DatabaseManagement.ExceptionHandler.DatabaseExceptionHandler;

namespace Persistence.ServiceRepository.Queries.ServiceManagementRepository
{
    //implemented class to centeralize all Company table Query (Select)  methods
    public class CompanyRepository_Query : ICompanyRepository_Query
    {
        private readonly IDatabaseContext _databaseContext;
        public CompanyRepository_Query(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Company?> FindCompanyByIdAsync(Guid companyId)
        {
            try
            {
                var company = await _databaseContext.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);

                return company;
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<List<Company>?> GetCompanyListAsync()
        {
            try
            {
                return await _databaseContext.Companies.ToListAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }

        public async Task<Company?> GetCompanyDetailAsync(Guid companyId)
        {
            try
            {
                return await _databaseContext.Companies.Where(c => c.CompanyId.Equals(companyId))
                    .Include(c => c.Users)
                    .Include(c => c.SupplyRelationsAsSupplier)
                    .Include(c => c.SupplyRelationsAsConsumer)
                    .FirstAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
                return null;
            }
        }
    }
}

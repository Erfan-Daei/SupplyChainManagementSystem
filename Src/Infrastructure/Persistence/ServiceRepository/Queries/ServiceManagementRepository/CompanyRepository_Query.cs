using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Domain.Entities.ServiceManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseManagement.DatabaseConfiguration;

namespace Persistence.ServiceRepository.Queries.ServiceManagementRepository
{
    //implemented class to centeralize all Company table Query (Select)  methods
    public class CompanyRepository_Query : ICompanyRepository_Query
    {
        private readonly DatabaseContext _databaseContext;
        public CompanyRepository_Query(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Company?> FindCompanyByIdAsync(Guid companyId)
        {
            var company = await _databaseContext.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);

            return company;
        }
    }
}

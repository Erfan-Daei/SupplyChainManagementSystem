using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Domain.Entities.ServiceManagement;
using Persistence.DatabaseManagement.DatabaseConfiguration.Context;
using Persistence.DatabaseManagement.ExceptionHandler.DatabaseExceptionHandler;

namespace Persistence.ServiceRepository.Commands.ServiceManagementRepository
{
    //All Company Command services
    public class CompanyRepository_Command : ICompanyRepository_Command
    {
        private readonly IDatabaseContext _databaseContext;
        public CompanyRepository_Command(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AddCompanyAsync(Company company)
        {
            try
            {
                await _databaseContext.Companies.AddAsync(company);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                DatabaseExceptionHandler.Handle(ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}

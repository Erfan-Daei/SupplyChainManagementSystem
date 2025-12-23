using Domain.Entities.LogManagement;
using Domain.Entities.ServiceManagement;
using Domain.Entities.UserManagement;

namespace Application.Interfaces.Database
{
    public interface IDatabaseContext   //interface to use for EF queries
    {
        IQueryable<User> Users { get; }
        IQueryable<Role> Roles { get; }
        IQueryable<UserInRole> UserInRoles { get; }
        IQueryable<Company> Companies { get; }
        IQueryable<Service> Services { get; }
        IQueryable<SupplyRelation> SupplyRelations { get; }
        IQueryable<Audit> Audits { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

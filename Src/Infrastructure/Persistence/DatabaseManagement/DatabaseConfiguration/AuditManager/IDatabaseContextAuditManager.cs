using Domain.Entities.LogManagement;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.DatabaseManagement.DatabaseConfiguration.AuditManager
{
    public interface IDatabaseContextAuditManager
    {
        IEnumerable<Audit> CreateAudits(IEnumerable<EntityEntry> entries);
    }
}

using Domain.Entities.LogManagement;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.Interface.DatabaseManagement.DatabaseConfiguration
{
    public interface IDatabaseContextAuditManager
    {
        IEnumerable<Audit> CreateAudits(IEnumerable<EntityEntry> entries);
    }
}

using Domain.Entities.LogManagement;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.DatabaseManagement.DatabaseConfiguration.AuditManager;

namespace Presentation_Test.Setup.Database
{
    public class FakeDatabaseContextAuditManager : IDatabaseContextAuditManager
    {
        public IEnumerable<Audit> CreateAudits(IEnumerable<EntityEntry> entries)
        {
            return Enumerable.Empty<Audit>();
        }
    }
}

using Domain.Entities.Common;
using Domain.Entities.UserManagement;

namespace Domain.Entities.ServiceManagement
{
    public class Company : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }

        // 1 company to many users
        public ICollection<User> Users { get; set; } = new List<User>();

        // 1 compny to many services
        public ICollection<Service> Services { get; set; } = new List<Service>();

        // 1 company to many supplyRelations
        public ICollection<SupplyRelation> SupplyRelations { get; set; } = new List<SupplyRelation>(); 
    }
}

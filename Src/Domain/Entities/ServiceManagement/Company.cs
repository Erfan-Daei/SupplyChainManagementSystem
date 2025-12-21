using Domain.Entities.Common;
using Domain.Entities.UserManagement;

namespace Domain.Entities.ServiceManagement
{
    public class Company : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<SupplyRelation> SupplyRelations { get; set; } = new List<SupplyRelation>();
    }
}

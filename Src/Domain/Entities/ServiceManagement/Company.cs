using Domain.Entities.Common;
using Domain.Entities.UserManagement;

namespace Domain.Entities.ServiceManagement
{
    public class Company : BaseEntity
    {
        public Guid CompanyId { get; set; } = Guid.NewGuid();
        public string CompanyName { get; set; } = null!;

        // 1 company to many users
        public ICollection<User> Users { get; set; } = [];

        //many companies to many services
        public ICollection<Service> Services { get; set; } = [];

        // 1 company to many supplyRelations as "Supplier" and as "Consumer"
        public ICollection<SupplyRelation> SupplyRelationsAsSupplier { get; set; } = [];

        public ICollection<SupplyRelation> SupplyRelationsAsConsumer { get; set; } = [];

        //creator method
        public static Company Create(string companyName)
        {
            if (string.IsNullOrEmpty(companyName))
                throw new ArgumentNullException("نام شرکت نمی تواند خالی باشد");

            return new Company
            {
                CompanyId = Guid.NewGuid(),
                CompanyName = companyName,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static Company Edit(Company company, string companyName)
        {
            if (string.IsNullOrEmpty(companyName))
                throw new ArgumentNullException("نام شرکت نمی تواند خالی باشد");

            company.CompanyName = companyName;
            company.SetUpdatedAt();
            return company;
        }
    }
}

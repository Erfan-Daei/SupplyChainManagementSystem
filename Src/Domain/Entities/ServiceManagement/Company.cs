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

        //Edit Method
        public static Company Edit(Company company, string companyName)
        {
            if (string.IsNullOrEmpty(companyName))
                throw new ArgumentNullException("نام شرکت نمی تواند خالی باشد");

            company.CompanyName = companyName;
            company.SetUpdatedAt();
            return company;
        }

        //remove Service from Company SupplierService list
        public static Company RemoveService(Company company, Service service)
        {
            if (service == null || company == null)
                throw new ArgumentNullException("نام شرکت نمی تواند خالی باشد");

            company.Services.Remove(service);
            company.SetUpdatedAt();
            return company;
        }

        //add Service from Company SupplierService list
        public void AddService(Service service)
        {
            if (service == null)
                throw new ArgumentNullException("نام شرکت نمی تواند خالی باشد");

            Services.Add(service);
            SetUpdatedAt();
        }
    }
}

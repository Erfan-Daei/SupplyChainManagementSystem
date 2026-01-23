using Domain.Entities.Common;

namespace Domain.Entities.ServiceManagement
{
    public class Service : BaseEntity
    {
        public Guid ServiceId { get; set; } = Guid.NewGuid();
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public bool ServiceIsActive { get; set; } = true;

        public bool ServiceIsConfirmed { get; set; } = false;

        public Guid CreatorCompanyId { get; set; }

        //many services to many companies
        public ICollection<Company> SupplierCompanies { get; set; } = [];

        // 1 service to many supplyRelation
        public ICollection<SupplyRelation> SupplyRelations { get; set; } = [];

        public void ChangeServiceIsActiveState()   //method for automated serviceActivation update
        {
            ServiceIsActive = !ServiceIsActive;
            SetUpdatedAt();
        }

        //creator method
        public static Service Create(string serviceName, string serviceDescription, Guid creatorCompanyId)
        {
            if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(serviceDescription) || creatorCompanyId == Guid.Empty)
                throw new ArgumentNullException("لطفا تمام مقادیر را پر کنید");

            return new Service()
            {
                ServiceId = Guid.NewGuid(),
                ServiceName = serviceName,
                ServiceDescription = serviceDescription,
                ServiceIsActive = true,
                ServiceIsConfirmed = false,
                CreatorCompanyId = creatorCompanyId,
                CreatedAt = DateTime.UtcNow
            };
        }

        //edit method
        public void Edit(string serviceName, string serviceDescription)
        {
            if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(serviceDescription))
                throw new ArgumentNullException("لطفا تمام مقادیر را پر کنید");

            ServiceName = serviceName;
            ServiceDescription = serviceDescription;
            SetUpdatedAt();
        }

        //add new SupplierCompany
        public void AddSupplierCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException("لطفا تمام مقادیر را پر کنید");

            SupplierCompanies.Add(company);
            SetUpdatedAt();
        }

        //remove Company from SupplierCompany list
        public void RemoveSupplierCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException("لطفا تمام مقادیر را پر کنید");

            SupplierCompanies.Remove(company);
            SetUpdatedAt();
        }

        //Confirm Service
        public void ConfirmService()
        {
            ServiceIsConfirmed = true;
            SetUpdatedAt();
        }
    }
}

using Domain.Entities.Common;

namespace Domain.Entities.ServiceManagement
{
    public class Service : BaseEntity
    {
        public Guid ServiceId { get; set; } = Guid.NewGuid();
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public bool ServiceIsActive { get; set; } = true;

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
                CreatorCompanyId = creatorCompanyId,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static Service Edit(Service service, string serviceName, string serviceDescription, bool serviceIsActive)
        {
            if (service == null || string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(serviceDescription))
                throw new ArgumentNullException("لطفا تمام مقادیر را پر کنید");

            service.ServiceName = serviceName;
            service.ServiceDescription = serviceDescription;
            service.ServiceIsActive = serviceIsActive;
            service.SetDeletedAt();
            return service;
        }

        public static Service Delete(Service service)
        {
            if (service == null)
                throw new ArgumentNullException("لطفا تمام مقادیر را پر کنید");

            service.ServiceIsActive = false;
            service.SetDeletedAt();
            return service;
        }
    }
}

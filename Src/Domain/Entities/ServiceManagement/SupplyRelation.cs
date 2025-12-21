using Domain.Entities.Common;

namespace Domain.Entities.ServiceManagement
{
    public class SupplyRelation : BaseEntity
    {
        public Guid SupplyRelationId { get; set; }

        public bool SupplyRelationIsActive { get; set; } = true;
        public void SetSupplyRelationIsActive()
        {
            SupplyRelationIsActive = !SupplyRelationIsActive;
            SetUpdatedAt();
        }

        public Service Service { get; set; }
        public Guid ServiceId { get; set; }

        public Company ConsumerCompany { get; set; }
        public Guid ConsumerCompanyId { get; set; }

        public SupplyRelation(Service service, Company consumerCompany)
        {
            if (service.SupplierCompanyId == consumerCompany.CompanyId)
            {
                throw new InvalidOperationException("سرویس دهنده و سرویس گیرنده نمیتوانند یکسان باشند");
            }
            SupplyRelationId = Guid.NewGuid();
            Service = service;
            ServiceId = service.ServiceId;
            ConsumerCompany = consumerCompany;
            ConsumerCompanyId = consumerCompany.CompanyId;
        }

        public SupplyRelation() { }
    }
}

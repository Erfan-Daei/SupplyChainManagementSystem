using Domain.Entities.Common;

namespace Domain.Entities.ServiceManagement
{
    public class SupplyRelation : BaseEntity   //table to save companies and services relations
    {
        public Guid SupplyRelationId { get; set; }

        public bool SupplyRelationIsActive { get; set; } = true;
        public void SetSupplyRelationIsActive()   //method for automated supplyRelationActivation update
        {
            SupplyRelationIsActive = !SupplyRelationIsActive;
            SetUpdatedAt();
        }

        public Service Service { get; set; }
        public Guid ServiceId { get; set; }

        public Company ConsumerCompany { get; set; }
        public Guid ConsumerCompanyId { get; set; }

        //enforced methode to check companies SELF supplyRelation
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

        public SupplyRelation() { } //empty cunstructor for EF migrations run properly
    }
}

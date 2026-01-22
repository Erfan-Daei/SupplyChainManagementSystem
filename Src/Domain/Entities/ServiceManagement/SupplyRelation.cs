using Domain.Entities.Common;

namespace Domain.Entities.ServiceManagement
{
    public class SupplyRelation : BaseEntity   //table to save companies and services relations
    {
        public Guid SupplyRelationId { get; set; } = Guid.NewGuid();
        public bool SupplyRelationIsActive { get; set; } = true;

        public Service Service { get; set; } = null!;
        public Guid ServiceId { get; set; }

        public Company SupplierCompany { get; set; } = null!;
        public Guid SupplierCompanyId { get; set; }

        public Company ConsumerCompany { get; set; } = null!;
        public Guid ConsumerCompanyId { get; set; }

        public void ChangeSupplyRelationIsActiveState()   //method for automated supplyRelationActivation update
        {
            SupplyRelationIsActive = !SupplyRelationIsActive;
            SetUpdatedAt();
        }

        public void DeActiveSupplyRelation()   //method for automated supplyRelationDeActivation
        {
            SupplyRelationIsActive = false;
            SetUpdatedAt();
        }

        //creator method
        public static SupplyRelation Create(Guid serviceId, Guid supplierCompanyId, Guid consumerCompanyId)
        {
            if (serviceId ==  Guid.Empty || supplierCompanyId == Guid.Empty || consumerCompanyId == Guid.Empty)
                throw new ArgumentNullException("لطفا تمام مقادیر را پر کنید");

            if (supplierCompanyId == consumerCompanyId)
                throw new InvalidOperationException("سرویس دهنده و سرویس گیرنده نمیتوانند یکسان باشند");

            return new SupplyRelation
            {
                SupplyRelationId = Guid.NewGuid(),
                SupplyRelationIsActive = true,
                ServiceId = serviceId,
                SupplierCompanyId = supplierCompanyId,
                ConsumerCompanyId = consumerCompanyId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}

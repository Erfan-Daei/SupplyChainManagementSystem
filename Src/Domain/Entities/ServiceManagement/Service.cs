using Domain.Entities.Common;

namespace Domain.Entities.ServiceManagement
{
    public class Service : BaseEntity
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }

        public bool ServiceIsActive { get; set; } = false;
        public void SetServiceIsActive()
        {
            ServiceIsActive = !ServiceIsActive;
            SetUpdatedAt();
        }

        public Company SupplierCompany { get; set; }
        public Guid SupplierCompanyId { get; set; }

        public ICollection<SupplyRelation> SupplyRelations { get; set; } = new List<SupplyRelation>();
    }
}

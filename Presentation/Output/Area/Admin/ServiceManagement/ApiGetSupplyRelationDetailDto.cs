namespace Presentation.Output.Area.Admin.ServiceManagement
{
    public class ApiGetSupplyRelationDetailDto
    {
        public bool SupplyRelationIsActive { get; set; }

        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;

        public Guid SupplierCompanyId { get; set; }
        public string SupplierCompanyName { get; set; } = null!;

        public Guid ConsumerCompanyId { get; set; }
        public string ConsumerCompanyName { get; set; } = null!;
    }
}

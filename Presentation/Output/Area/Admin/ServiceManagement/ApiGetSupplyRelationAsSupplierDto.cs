namespace Presentation.Output.Area.Admin.ServiceManagement
{
    public class ApiGetSupplyRelationAsSupplierDto
    {
        public Guid SupplyRelationId { get; set; }
        public string ConsumerCompanyName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public bool SupplyRelationIsConfirmed { get; set; }
    }
}

namespace Presentation.Output.Area.Admin.ServiceManagement
{
    public class ApiGetSupplyRelationAsConsumerDto
    {
        public Guid SupplyRelationId { get; set; }
        public string SupplierCompanyName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public bool SupplyRelationIsConfirmed { get; set; }
    }
}

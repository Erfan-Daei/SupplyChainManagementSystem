namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationAsConsumer
{
    public class GetSupplyRelationAsConsumerResultDto
    {
        public Guid SupplyRelationId { get; set; }
        public string SupplierCompanyName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public bool SupplyRelationIsConfirmed { get; set; }
    }
}

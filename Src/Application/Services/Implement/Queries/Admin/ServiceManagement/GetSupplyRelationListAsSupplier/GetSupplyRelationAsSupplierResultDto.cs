namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationListAsSupplier
{
    public class GetSupplyRelationAsSupplierResultDto
    {
        public Guid SupplyRelationId { get; set; }
        public string ConsumerCompanyName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public bool SupplyRelationIsConfirmed { get; set; }
    }
}

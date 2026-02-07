namespace Application.Services.Implement.Queries.Users.ServiceManagement.GetUnConfirmedSupplyRelationList
{
    public class GetUnConfirmedSupplyRelationListResultDto
    {
        public Guid SupplyRelationId { get; set; }
        public string SupplierCompanyName { get; set; } = null!;
        public string ConsumerCompanyName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

namespace Application.Services.Implement.Queries.Admin.GetCompanyDetail
{
    public class GetCompanyDetailResultDto
    {
        public string CompanyName { get; set; } = null!;
        public int UserCount { get; set; }
        public int AsSupplierCount { get; set; }
        public int AsConsumerCount { get; set; }
    }
}

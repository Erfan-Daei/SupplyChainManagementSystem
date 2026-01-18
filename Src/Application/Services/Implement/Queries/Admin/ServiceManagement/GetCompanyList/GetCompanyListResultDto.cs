namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyList
{
    public class GetCompanyListResultDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
    }
}

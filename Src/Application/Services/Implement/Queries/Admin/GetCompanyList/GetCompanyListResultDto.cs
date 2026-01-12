namespace Application.Services.Implement.Queries.Admin.GetCompanyList
{
    public class GetCompanyListResultDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
    }
}

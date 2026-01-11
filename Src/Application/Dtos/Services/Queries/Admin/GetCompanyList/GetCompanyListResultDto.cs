namespace Application.Dtos.Services.Queries.Admin.GetCompanyList
{
    public class GetCompanyListResultDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
    }
}

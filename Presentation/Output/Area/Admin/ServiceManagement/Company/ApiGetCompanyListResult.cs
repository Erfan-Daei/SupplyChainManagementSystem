using Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyList;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.Company
{
    public class ApiGetCompanyListValues
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
    }
    public class ApiGetCompanyListResult
    {
        public List<ApiGetCompanyListValues> Values { get; set; } = [];

        public List<LinkDto> Links { get; set; } = [];

        public static ApiGetCompanyListResult Result(List<GetCompanyListResultDto>? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiGetCompanyListResult();

            return new ApiGetCompanyListResult
            {
                Values = resultDto.Select(c => new ApiGetCompanyListValues
                {
                    CompanyId = c.CompanyId,
                    CompanyName = c.CompanyName,
                }).ToList(),

                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("GetCompanyDetail", "CompanyManager", new {companyId = "CompanyId"})!,
                        Method = "GET" ,
                        Rel = "Self"
                    },
                }
            };
        }
    }
}

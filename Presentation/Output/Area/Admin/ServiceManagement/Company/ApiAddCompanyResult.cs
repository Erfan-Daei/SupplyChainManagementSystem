using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.Company
{
    public class ApiAddCompanyResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiAddCompanyResult Result(Guid? companyId, IUrlHelper url)
        {
            if (companyId == null)
                return new ApiAddCompanyResult();

            return new ApiAddCompanyResult
            {
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("GetCompanyDetail", "CompanyManager", new {companyId = companyId})!,
                        Method = "GET" ,
                        Rel = "Self"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("EditCompany", "CompanyManager", new {companyId = companyId, companyName = "Company Name"})!,
                        Method = "PUT" ,
                        Rel = "Edit"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("DeleteCompany", "CompanyManager", new {companyId = companyId})!,
                        Method = "DELETE" ,
                        Rel = "Delete"
                    },
                }
            };
        }
    }
}

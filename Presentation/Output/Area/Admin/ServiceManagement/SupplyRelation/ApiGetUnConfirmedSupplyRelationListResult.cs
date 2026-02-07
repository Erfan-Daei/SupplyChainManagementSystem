using Application.Services.Implement.Queries.Users.ServiceManagement.GetUnConfirmedSupplyRelationList;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.SupplyRelation
{
    public class ApiGetUnConfirmedSupplyRelationListResultValues
    {
        public Guid SupplyRelationId { get; set; }
        public string SupplierCompanyName { get; set; } = null!;
        public string ConsumerCompanyName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
    public class ApiGetUnConfirmedSupplyRelationListResult
    {
        public List<ApiGetUnConfirmedSupplyRelationListResultValues> SupplyRelations { get; set; } = [];

        public List<LinkDto> Links { get; set; } = [];

        public static ApiGetUnConfirmedSupplyRelationListResult Result(List<GetUnConfirmedSupplyRelationListResultDto>? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiGetUnConfirmedSupplyRelationListResult();
            return new ApiGetUnConfirmedSupplyRelationListResult
            {
                SupplyRelations = resultDto.Select(sr => new ApiGetUnConfirmedSupplyRelationListResultValues
                {
                    SupplyRelationId = sr.SupplyRelationId,
                    SupplierCompanyName = sr.SupplierCompanyName,
                    ConsumerCompanyName = sr.ConsumerCompanyName,
                    ServiceName = sr.ServiceName,
                    CreatedAt = sr.CreatedAt,
                }).ToList(),

                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("GetSupplyRelationDetail", "SupplyRelationManager", new {supplyRelationId = "SupplyRelationId"})!,
                        Method = "GET" ,
                        Rel = "Self"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("GetCompanyDetail", "CompanyManager", new {companyId = "CompanyId"})!,
                        Method = "GET" ,
                        Rel = "SupplierCompanyDetail"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("GetCompanyDetail", "CompanyManager", new {companyId = "CompanyId"})!,
                        Method = "GET" ,
                        Rel = "SupplierCompanyDetail"
                    }
                }
            };
        }
    }
}

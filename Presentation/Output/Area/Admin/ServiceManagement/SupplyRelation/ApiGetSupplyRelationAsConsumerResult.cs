using Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationAsConsumer;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.SupplyRelation
{
    public class ApiGetSupplyRelationAsConsumerValues
    {
        public Guid SupplyRelationId { get; set; }
        public string SupplierCompanyName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public bool SupplyRelationIsConfirmed { get; set; }
    }
    public class ApiGetSupplyRelationAsConsumerResult
    {
        public List<ApiGetSupplyRelationAsConsumerValues> SupplyRelations { get; set; } = [];

        public List<LinkDto> Links { get; set; } = [];

        public static ApiGetSupplyRelationAsConsumerResult Result(Guid companyId, List<GetSupplyRelationAsConsumerResultDto>? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiGetSupplyRelationAsConsumerResult();

            return new ApiGetSupplyRelationAsConsumerResult
            {
                SupplyRelations = resultDto.Select(sr => new ApiGetSupplyRelationAsConsumerValues
                {
                    SupplyRelationId = sr.SupplyRelationId,
                    SupplierCompanyName = sr.SupplierCompanyName,
                    ServiceName = sr.ServiceName,
                    SupplyRelationIsConfirmed = sr.SupplyRelationIsConfirmed
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
                        Href = url.ActionLink("GetCompanyDetail", "CompanyManager", new {companyId = companyId})!,
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

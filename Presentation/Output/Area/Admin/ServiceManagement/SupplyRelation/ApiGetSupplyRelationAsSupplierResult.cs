using Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationListAsSupplier;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.SupplyRelation
{
    public class ApiGetSupplyRelationAsSupplierValues
    {
        public Guid SupplyRelationId { get; set; }
        public string ConsumerCompanyName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public bool SupplyRelationIsConfirmed { get; set; }
    }
    public class ApiGetSupplyRelationAsSupplierResult
    {
        public List<ApiGetSupplyRelationAsSupplierValues> SupplyRelations { get; set; } = [];

        public List<LinkDto> Links { get; set; } = [];

        public static ApiGetSupplyRelationAsSupplierResult Result(Guid companyId, List<GetSupplyRelationAsSupplierResultDto>? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiGetSupplyRelationAsSupplierResult();

            return new ApiGetSupplyRelationAsSupplierResult
            {
                SupplyRelations = resultDto.Select(sr => new ApiGetSupplyRelationAsSupplierValues
                {
                    SupplyRelationId = sr.SupplyRelationId,
                    ConsumerCompanyName = sr.ConsumerCompanyName,
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
                        Rel = "ConsumerCompanyDetail"
                    }
                }
            };
        }
    }
}

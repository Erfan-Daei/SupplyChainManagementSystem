using Application.Services.Implement.Queries.Admin.ServiceManagement.GetSupplyRelationDetail;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.SupplyRelation
{
    public class ApiGetSupplyRelationDetailResult
    {
        public bool SupplyRelationIsActive { get; set; }

        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;

        public Guid SupplierCompanyId { get; set; }
        public string SupplierCompanyName { get; set; } = null!;

        public Guid ConsumerCompanyId { get; set; }
        public string ConsumerCompanyName { get; set; } = null!;

        public List<LinkDto> Links { get; set; } = [];

        public static ApiGetSupplyRelationDetailResult Result(Guid supplyRelationId, GetSupplyRelationDetailResultDto? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiGetSupplyRelationDetailResult();

            return new ApiGetSupplyRelationDetailResult
            {
                SupplyRelationIsActive = resultDto.SupplyRelationIsActive,
                ServiceId = resultDto.ServiceId,
                ServiceName = resultDto.ServiceName,
                ServiceDescription = resultDto.ServiceDescription,
                SupplierCompanyId = resultDto.SupplierCompanyId,
                SupplierCompanyName = resultDto.SupplierCompanyName,
                ConsumerCompanyId = resultDto.ConsumerCompanyId,
                ConsumerCompanyName = resultDto.ConsumerCompanyName,

                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Href = url.ActionLink("GetSupplyRelationDetail", "SupplyRelationManager", new {supplyRelationId = supplyRelationId})!,
                        Method = "GET" ,
                        Rel = "Self"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("GetSupplyRelationAsSupplier", "SupplyRelationManager", new {companyId = "CompanyId"})!,
                        Method = "GET" ,
                        Rel = "SupplierList"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("GetSupplyRelationAsConsumer", "SupplyRelationManager", new {companyId = "CompanyId"})!,
                        Method = "GET" ,
                        Rel = "ConsumerList"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("GetCompanyDetail", "CompanyManager", new {companyId = "CompanyId"})!,
                        Method = "GET" ,
                        Rel = "CompanyDetail"
                    }
                }
            };
        }
    }
}

using Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyDetail;
using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.Company
{
    public class ApiGetCompanyDetailCompanyServicesValues
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
    }
    public class ApiGetCompanyDetaiResult
    {
        public string CompanyName { get; set; } = null!;
        public int UserCount { get; set; }
        public int AsSupplierCount { get; set; }
        public int AsConsumerCount { get; set; }
        public List<ApiGetCompanyDetailCompanyServicesValues> CompanyServices { get; set; } = [];

        public List<LinkDto> Links { get; set; } = [];

        public static ApiGetCompanyDetaiResult Result(Guid companyId, GetCompanyDetailResultDto? resultDto, IUrlHelper url)
        {
            if (resultDto == null)
                return new ApiGetCompanyDetaiResult();

            return new ApiGetCompanyDetaiResult
            {
                CompanyName = resultDto.CompanyName,
                UserCount = resultDto.UserCount,
                AsSupplierCount = resultDto.AsSupplierCount,
                AsConsumerCount = resultDto.AsConsumerCount,
                CompanyServices = resultDto.CompanyServices.Select(cs => new ApiGetCompanyDetailCompanyServicesValues
                {
                    ServiceId = cs.ServiceId,
                    ServiceName = cs.ServiceName
                }).ToList(),

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
                    new LinkDto
                    {
                        Href = url.ActionLink("GetSupplyRelationAsSupplier", "SupplyRelationManager", new {companyId = companyId})!,
                        Method = "GET" ,
                        Rel = "SupplyRelationList"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("GetSupplyRelationAsConsumer", "SupplyRelationManager", new {companyId = companyId})!,
                        Method = "GET" ,
                        Rel = "SupplyRelationList"
                    },
                    new LinkDto
                    {
                        Href = url.ActionLink("GetServiceDetail", "ServiceManager", new {serviceId = "ServiceId"})!,
                        Method = "GET" ,
                        Rel = "ServiceDetail"
                    },
                }
            };
        }
    }
}

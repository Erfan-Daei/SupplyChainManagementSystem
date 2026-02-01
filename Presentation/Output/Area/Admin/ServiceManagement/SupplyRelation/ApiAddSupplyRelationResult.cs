using Microsoft.AspNetCore.Mvc;
using Presentation.Output.Base;

namespace Presentation.Output.Area.Admin.ServiceManagement.SupplyRelation
{
    public class ApiAddSupplyRelationResult
    {
        public List<LinkDto> Links { get; set; } = [];

        public static ApiAddSupplyRelationResult Result(Guid? supplyRelationId, IUrlHelper url)
        {
            if (supplyRelationId == null)
                return new ApiAddSupplyRelationResult();

            return new ApiAddSupplyRelationResult
            {
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
                        Href = url.ActionLink("ConfirmSupplyRelation", "SupplyRelationManager", new {supplyRelationId = supplyRelationId})!,
                        Method = "PUT" ,
                        Rel = "Confirm"
                    },
                }
            };
        }
    }
}

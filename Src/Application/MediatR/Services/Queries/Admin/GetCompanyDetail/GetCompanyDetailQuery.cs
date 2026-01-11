using Application.Dtos.Services.Queries.Admin.GetCompanyDetail;
using Common.Output;
using MediatR;

namespace Application.MediatR.Services.Queries.Admin.GetCompanyDetail
{
    //MediatR Query for GetCompanyDetailService
    public record GetCompanyDetailQuery(Guid companyId) : IRequest<ResultDto<GetCompanyDetailResultDto>>;
}

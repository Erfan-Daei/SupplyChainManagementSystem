using Application.Services.Implement.Queries.Admin.GetCompanyDetail;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Queries.Admin.GetCompanyDetail
{
    //MediatR Query for GetCompanyDetailService
    public record GetCompanyDetailQuery(Guid companyId) : IRequest<ResultDto<GetCompanyDetailResultDto>>;

    //MediatR Handler for GetCompanyDetailService
    public class GetCompanyDetailQueryHandler : IRequestHandler<GetCompanyDetailQuery, ResultDto<GetCompanyDetailResultDto>>
    {
        private readonly IGetCompanyDetail _getCompanyDetail;
        public GetCompanyDetailQueryHandler(IGetCompanyDetail getCompanyDetail)
        {
            _getCompanyDetail = getCompanyDetail;
        }
        public async Task<ResultDto<GetCompanyDetailResultDto>> Handle(GetCompanyDetailQuery request, CancellationToken cancellationToken)
        {
            return await _getCompanyDetail.GetCompanyDetailAsync(request, cancellationToken);
        }
    }
}

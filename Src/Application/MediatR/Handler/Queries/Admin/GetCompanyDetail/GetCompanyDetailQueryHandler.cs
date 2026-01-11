using Application.Dtos.Services.Queries.Admin.GetCompanyDetail;
using Application.Interfaces.Services.Queries.Admin.GetCompanyDetail;
using Application.MediatR.Services.Queries.Admin.GetCompanyDetail;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Queries.Admin.GetCompanyDetail
{
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

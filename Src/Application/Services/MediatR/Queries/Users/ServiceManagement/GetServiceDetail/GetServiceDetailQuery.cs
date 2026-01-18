using Application.Services.Implement.Queries.Users.ServiceManagement.GetServiceDetail;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Queries.Users.ServiceManagement.GetServiceDetail
{
    //MeddiatR Query for GetServiceDetailService
    public record GetServiceDetailQuery(Guid serviceId) : IRequest<ResultDto<GetServiceDetailResultDto>>;

    //MeddiatR QueryHandler for GetServiceDetailService
    public class GetServiceDetailQueryHandler : IRequestHandler<GetServiceDetailQuery, ResultDto<GetServiceDetailResultDto>>
    {
        private readonly IGetServiceDetail _getServiceDetail;
        public GetServiceDetailQueryHandler(IGetServiceDetail getServiceDetail)
        {
            _getServiceDetail = getServiceDetail;
        }
        public async Task<ResultDto<GetServiceDetailResultDto>> Handle(GetServiceDetailQuery request, CancellationToken cancellationToken)
        {
            return await _getServiceDetail.GetServiceDetailAsync(request, cancellationToken);
        }
    }
}

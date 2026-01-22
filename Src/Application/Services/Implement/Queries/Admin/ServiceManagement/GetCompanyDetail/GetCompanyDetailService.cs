using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Queries.Admin.ServiceManagement.GetCompanyDetail;
using Common.Output;
using System.Net;

namespace Application.Services.Implement.Queries.Admin.ServiceManagement.GetCompanyDetail
{
    public class GetCompanyDetailService : IGetCompanyDetail
    {
        private readonly ICompanyRepository_Query _company_Query;   //GetCompanyDetailAsync
        public GetCompanyDetailService(ICompanyRepository_Query company_Query)
        {
            _company_Query = company_Query;
        }
        public async Task<ResultDto<GetCompanyDetailResultDto>> GetCompanyDetailAsync(GetCompanyDetailQuery request, CancellationToken ct)
        {
            try
            {
                var company = await _company_Query.GetCompanyDetailAsync(request.companyId);
                if (company == null)
                    return ResultDto<GetCompanyDetailResultDto>.Failed(ResultDtoMessageLibrary.CompanyNotFound, HttpStatusCode.NotFound);

                var MappedCompany = new GetCompanyDetailResultDto
                {
                    CompanyName = company.CompanyName,
                    UserCount = company.Users.Count,
                    AsSupplierCount = company.SupplyRelationsAsSupplier.Count,
                    AsConsumerCount = company.SupplyRelationsAsConsumer.Count,
                    CompanyServices = company.Services.Select(s => new GetCompanyDetailCompanyServicesDto
                    {
                        ServiceId = s.ServiceId,
                        ServiceName = s.ServiceName
                    }).ToList()
                };

                return ResultDto<GetCompanyDetailResultDto>.Succeeded(MappedCompany, ResultDtoMessageLibrary.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<GetCompanyDetailResultDto>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

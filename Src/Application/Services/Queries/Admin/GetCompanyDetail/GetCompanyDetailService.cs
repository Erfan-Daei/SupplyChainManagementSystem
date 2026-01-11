using Application.Dtos.Services.Queries.Admin.GetCompanyDetail;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Interfaces.Services.Queries.Admin.GetCompanyDetail;
using Application.MediatR.Services.Queries.Admin.GetCompanyDetail;
using Common.Output;
using System.Net;

namespace Application.Services.Queries.Admin.GetCompanyDetail
{
    public class GetCompanyDetailService : IGetCompanyDetail
    {
        private readonly ICompanyRepository_Query _company_Query;
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
                    return ResultDto<GetCompanyDetailResultDto>.Failed("شرکت مورد نظر یافت نشد", HttpStatusCode.NotFound);

                var MappedCompany = new GetCompanyDetailResultDto
                {
                    CompanyName = company.CompanyName,
                    UserCount = company.Users.Count,
                    AsSupplierCount = company.SupplyRelationsAsSupplier.Count,
                    AsConsumerCount = company.SupplyRelationsAsConsumer.Count,
                };

                return ResultDto<GetCompanyDetailResultDto>.Succeeded(MappedCompany, "اطلاعات شرکت شما", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResultDto<GetCompanyDetailResultDto>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

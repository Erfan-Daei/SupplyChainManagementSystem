using Application.Services.Implement.Commands.Admin.ServiceManagement.DeleteCompany;
using Common.Output;
using MediatR;

namespace Application.Services.MediatR.Commands.Admin.ServiceManagement.DeleteCompany
{
    //MediatR Command for DeleteCompanyService
    public record DeleteCompanyCommand(Guid companyId) : IRequest<ResultDto>;

    //MediatR commandHandler for DeleteCompany service
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, ResultDto>
    {
        private readonly IDeleteCompany _deleteCompany;
        public DeleteCompanyCommandHandler(IDeleteCompany deleteCompany)
        {
            _deleteCompany = deleteCompany;
        }
        public async Task<ResultDto> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _deleteCompany.DeleteCompanyAsync(request, cancellationToken);
        }
    }
}

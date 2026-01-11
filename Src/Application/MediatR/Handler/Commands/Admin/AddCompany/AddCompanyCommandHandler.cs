using Application.Interfaces.Services.Commands.Admin.AddCompany;
using Application.MediatR.Services.Commands.Admin.AddCompany;
using Common.Output;
using MediatR;

namespace Application.MediatR.Handler.Commands.Admin.AddCompany
{
    //MediatR CommandHandler for AddCompanyService
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, ResultDto<Guid>>
    {
        private readonly IAddCompany _addCompany;
        public AddCompanyCommandHandler(IAddCompany addCompany)
        {
            _addCompany = addCompany;
        }
        public async Task<ResultDto<Guid>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _addCompany.AddCompanyAsync(request, cancellationToken);
        }
    }
}

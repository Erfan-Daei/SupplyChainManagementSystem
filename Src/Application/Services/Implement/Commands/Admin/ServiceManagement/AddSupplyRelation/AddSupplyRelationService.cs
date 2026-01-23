using Application.Interfaces.Database.ServiceRepository.Commands.ServiceManagementRepository;
using Application.Interfaces.Database.ServiceRepository.Querries.ServiceManagementRepository;
using Application.Services.MediatR.Commands.Admin.ServiceManagement.AddSupplyRelation;
using Common.Output;
using Domain.Entities.ServiceManagement;
using System.Net;

namespace Application.Services.Implement.Commands.Admin.ServiceManagement.AddSupplyRelation
{
    public class AddSupplyRelationService : IAddSupplyRelation
    {
        private readonly IServiceRepository_Query _service_Query;   //GetServiceByIdAsync   GetCompanyWithServiceByIdAsync
        private readonly ICompanyRepository_Query _company_Query;   //GetCompanyByIdAsync
        private readonly ISupplyRelationRepository_Command _supplyRelatio_Command;   //AddSupplyRelationAync
        private readonly ISupplyRelationRepository_Query _supplyRelatio_Query;   //GetNextRelationsAsync
        public AddSupplyRelationService(IServiceRepository_Query service_Query
            , ICompanyRepository_Query company_Query
            , ISupplyRelationRepository_Command supplyRelation_Command
            , ISupplyRelationRepository_Query supplyRelation_Query)
        {
            _service_Query = service_Query;
            _company_Query = company_Query;
            _supplyRelatio_Command = supplyRelation_Command;
            _supplyRelatio_Query = supplyRelation_Query;
        }
        public async Task<ResultDto<Guid>> AddSupplyRelationAsync(AddSupplyRelationCommandRequest request, CancellationToken ct)
        {
            try
            {
                var service = await _service_Query.GetServiceByIdAsync(request.serviceId);
                if (service == null)
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.ServiceNotFound, HttpStatusCode.NotFound);

                var supplier = await _company_Query.GetCompanyWithServiceByIdAsync(request.supplierCompanyId);
                if (supplier == null)
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.SupplierCompanyNotFound, HttpStatusCode.NotFound);

                if (!supplier.Services.Any(s => s.ServiceId == service.ServiceId))
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.ServiceNotExistInCompany, HttpStatusCode.NotFound);

                var consumer = await _company_Query.GetCompanyByIdAsync(request.consumerCompanyId);
                if (consumer == null)
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.ConsumerCompanyNotFound, HttpStatusCode.NotFound);

                //cycle is invalid
                var checkCycle = await CheckCycle(request.supplierCompanyId, request.consumerCompanyId);
                if (checkCycle)
                    return ResultDto<Guid>.Failed(ResultDtoMessageLibrary.InvalidOperationBecauseOfCycle, HttpStatusCode.Conflict);

                var supplyRelation = SupplyRelation.Create(request.serviceId, request.supplierCompanyId, request.consumerCompanyId);

                await _supplyRelatio_Command.AddSupplyRelationAync(supplyRelation);

                return ResultDto<Guid>.Succeeded(supplyRelation.SupplyRelationId, ResultDtoMessageLibrary.SupplyRelationCreated, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ResultDto<Guid>.Failed(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        //check cycle
        public async Task<bool> CheckCycle(Guid supplierCompanyId, Guid consumerCompanyId)
        {
            if (supplierCompanyId == consumerCompanyId)
                return true;

            var visited = new HashSet<Guid>();
            return await HasPath(consumerCompanyId, supplierCompanyId, visited);
        }

        //from ConsumerCompany tries to reach SupplierCompany
        private async Task<bool> HasPath(Guid current, Guid target, HashSet<Guid> visited)
        {
            if (current == target)
                return true;

            if (!visited.Add(current))
                return false;

            var nextRelations = await _supplyRelatio_Query.GetNextRelationsAsync(current);

            if (nextRelations != null)
            {
                foreach (var next in nextRelations)
                {
                    if (await HasPath(next, target, visited))
                        return true;
                }
            }

            return false;
        }
    }
}

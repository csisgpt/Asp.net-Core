using InsuranceSuite.Application.Common.Exceptions;
using InsuranceSuite.Application.Common.Interfaces;
using InsuranceSuite.Domain.Entities;
using InsuranceSuite.Domain.Enums;
using MediatR;

namespace InsuranceSuite.Application.Features.PreRegistrations.Commands.UpdatePreRegistration;

public record UpdatePreRegistrationCommand(Guid Id, Guid ContractId, Guid SubContractId, Guid? HealthPlanId, string Category) : IRequest;

public class UpdatePreRegistrationCommandHandler : IRequestHandler<UpdatePreRegistrationCommand>
{
    private readonly IRepository<PreRegistration> _preRepo;
    private readonly IRepository<SubContract> _subRepo;
    private readonly IRepository<Contract> _contractRepo;
    private readonly IRepository<HealthPlan> _planRepo;

    public UpdatePreRegistrationCommandHandler(
        IRepository<PreRegistration> preRepo,
        IRepository<SubContract> subRepo,
        IRepository<Contract> contractRepo,
        IRepository<HealthPlan> planRepo)
    {
        _preRepo = preRepo;
        _subRepo = subRepo;
        _contractRepo = contractRepo;
        _planRepo = planRepo;
    }

    public async Task<Unit> Handle(UpdatePreRegistrationCommand request, CancellationToken cancellationToken)
    {
        var pre = await _preRepo.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("PreRegistration not found");

        var contract = await _contractRepo.GetByIdAsync(request.ContractId, cancellationToken)
            ?? throw new NotFoundException("Contract not found");

        var sub = await _subRepo.GetByIdAsync(request.SubContractId, cancellationToken)
            ?? throw new NotFoundException("SubContract not found");

        if (sub.ContractId != contract.Id)
            throw new ConflictException("SubContract does not belong to contract");

        if (sub.InsuranceType == InsuranceType.HealthSupplement)
        {
            if (request.HealthPlanId is null)
                throw new ConflictException("Health plan required for health supplement");

            var plan = await _planRepo.GetByIdAsync(request.HealthPlanId.Value, cancellationToken)
                ?? throw new NotFoundException("Health plan not found");

            if (plan.SubContractId != sub.Id)
                throw new ConflictException("Plan does not belong to subcontract");
        }
        else if (request.HealthPlanId is not null)
        {
            throw new ConflictException("Health plan must be null for this insurance type");
        }

        if (DateOnly.FromDateTime(DateTime.UtcNow) > sub.EndDate)
            throw new ConflictException("Registration window closed");

        pre.Update(request.ContractId, request.SubContractId, request.HealthPlanId, request.Category);
        await _preRepo.UpdateAsync(pre, cancellationToken);

        return Unit.Value;
    }
}

using InsuranceSuite.Domain.Enums;

namespace InsuranceSuite.Domain.Entities;

public class SubContract
{
    public Guid Id { get; private set; }
    public Guid ContractId { get; private set; }
    public InsuranceType InsuranceType { get; private set; }
    public decimal PremiumBase { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<HealthPlan> _plans = new();
    public IReadOnlyCollection<HealthPlan> Plans => _plans.AsReadOnly();

    private SubContract() { }

    public SubContract(Guid contractId, InsuranceType insuranceType, decimal premiumBase, DateOnly startDate, DateOnly endDate)
    {
        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date");

        Id = Guid.NewGuid();
        ContractId = contractId;
        InsuranceType = insuranceType;
        PremiumBase = premiumBase;
        StartDate = startDate;
        EndDate = endDate;
        IsActive = true;
    }

    public void AddHealthPlan(HealthPlan plan)
    {
        if (InsuranceType != InsuranceType.HealthSupplement)
            throw new InvalidOperationException("Health plans allowed only for HealthSupplement subcontracts");
        _plans.Add(plan);
    }
}

using InsuranceSuite.Domain.Enums;

namespace InsuranceSuite.Domain.Entities;

public class PreRegistration
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ContractId { get; private set; }
    public Guid SubContractId { get; private set; }
    public Guid? HealthPlanId { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public RegistrationStatus Status { get; private set; }
    public DateTime? LockedAt { get; private set; }
    public string? Notes { get; private set; }

    private readonly List<RegistrationAudit> _audits = new();
    public IReadOnlyCollection<RegistrationAudit> Audits => _audits.AsReadOnly();

    private PreRegistration() { }

    public PreRegistration(Guid userId, Guid contractId, Guid subContractId, Guid? healthPlanId, string category)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        ContractId = contractId;
        SubContractId = subContractId;
        HealthPlanId = healthPlanId;
        Category = category;
        CreatedAt = DateTime.UtcNow;
        Status = RegistrationStatus.Draft;
    }

    public void Submit()
    {
        if (Status != RegistrationStatus.Draft)
            throw new InvalidOperationException("Only drafts can be submitted");
        Status = RegistrationStatus.Submitted;
    }

    public void Approve()
    {
        if (Status != RegistrationStatus.Submitted)
            throw new InvalidOperationException("Only submitted registrations can be approved");
        Status = RegistrationStatus.Approved;
    }

    public void Lock()
    {
        LockedAt = DateTime.UtcNow;
        Status = RegistrationStatus.Locked;
    }
}

public class RegistrationAudit
{
    public Guid Id { get; private set; }
    public Guid PreRegistrationId { get; private set; }
    public Guid ActorUserId { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public DateTime ActionAt { get; private set; }
    public string DataJson { get; private set; } = string.Empty;

    private RegistrationAudit() { }

    public RegistrationAudit(Guid preRegistrationId, Guid actorUserId, string action, string dataJson)
    {
        Id = Guid.NewGuid();
        PreRegistrationId = preRegistrationId;
        ActorUserId = actorUserId;
        Action = action;
        ActionAt = DateTime.UtcNow;
        DataJson = dataJson;
    }
}

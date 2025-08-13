namespace InsuranceSuite.Domain.Entities;

public class HealthPlan
{
    public Guid Id { get; private set; }
    public Guid SubContractId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public decimal FullPremium { get; private set; }
    public decimal EmployeeQuota { get; private set; }
    public decimal CenterQuota { get; private set; }
    public decimal CompanyQuota { get; private set; }
    public decimal RefundableFees { get; private set; }
    public bool IsActive { get; private set; }

    private HealthPlan() { }

    public HealthPlan(Guid subContractId, string title, decimal fullPremium, decimal employeeQuota, decimal centerQuota, decimal companyQuota, decimal refundableFees)
    {
        Id = Guid.NewGuid();
        SubContractId = subContractId;
        Title = title;
        FullPremium = fullPremium;
        EmployeeQuota = employeeQuota;
        CenterQuota = centerQuota;
        CompanyQuota = companyQuota;
        RefundableFees = refundableFees;
        IsActive = true;
    }
}

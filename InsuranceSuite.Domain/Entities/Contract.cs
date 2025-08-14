namespace InsuranceSuite.Domain.Entities;

public class Contract
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public Guid CompanyId { get; private set; }
    public bool IsActive { get; private set; }

    private Contract() { }

    public Contract(string title, Guid companyId)
    {
        Id = Guid.NewGuid();
        Title = title;
        CompanyId = companyId;
        IsActive = true;
    }
}

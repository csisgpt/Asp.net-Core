namespace InsuranceSuite.Domain.Entities;

public class InsuranceCompany
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private InsuranceCompany() { }

    public InsuranceCompany(string name, string code)
    {
        Id = Guid.NewGuid();
        Name = name;
        Code = code;
        IsActive = true;
    }
}

namespace InsuranceSuite.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string NationalCode { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private User() { }

    public User(string nationalCode, string fullName, string email, string role, string passwordHash)
    {
        Id = Guid.NewGuid();
        NationalCode = nationalCode;
        FullName = fullName;
        Email = email;
        Role = role;
        PasswordHash = passwordHash;
        IsActive = true;
    }
}

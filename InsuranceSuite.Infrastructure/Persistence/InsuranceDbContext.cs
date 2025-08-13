using Microsoft.EntityFrameworkCore;
using InsuranceSuite.Domain.Entities;

namespace InsuranceSuite.Infrastructure.Persistence;

public class InsuranceDbContext : DbContext
{
    public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
    {
    }

    public DbSet<InsuranceCompany> InsuranceCompanies => Set<InsuranceCompany>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<SubContract> SubContracts => Set<SubContract>();
    public DbSet<HealthPlan> HealthPlans => Set<HealthPlan>();
    public DbSet<User> Users => Set<User>();
    public DbSet<PreRegistration> PreRegistrations => Set<PreRegistration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InsuranceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

using InsuranceSuite.Domain.Entities;
using InsuranceSuite.Domain.Enums;
using InsuranceSuite.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InsuranceSuite.Infrastructure.Seed;

public static class SeedData
{
    public static async Task InitializeAsync(InsuranceDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var admin = new User("0000000000", "Admin User", "admin@example.com", "Admin", "HASH");
        context.Users.Add(admin);

        var company = new InsuranceCompany("Sample Company", "SC01");
        context.InsuranceCompanies.Add(company);

        var contract = new Contract("Main Contract", company.Id);
        context.Contracts.Add(contract);

        var sub = new SubContract(contract.Id, InsuranceType.HealthSupplement, 1000000m, new DateOnly(2024,1,1), new DateOnly(2024,12,31));
        context.SubContracts.Add(sub);

        var plan = new HealthPlan(sub.Id, "Base Plan", 1000000m, 300000m, 300000m, 400000m, 0m);
        sub.AddHealthPlan(plan);
    }
}

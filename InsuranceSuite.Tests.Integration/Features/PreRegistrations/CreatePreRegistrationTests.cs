using System.Net.Http.Json;
using InsuranceSuite.Application.Features.PreRegistrations.Commands.CreatePreRegistration;
using InsuranceSuite.Infrastructure.Persistence;
using Xunit;

namespace InsuranceSuite.Tests.Integration.Features.PreRegistrations;

public class CreatePreRegistrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public CreatePreRegistrationTests(CustomWebApplicationFactory factory)
        => _factory = factory;

    [Fact]
    public async Task Creates_pre_registration()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<InsuranceDbContext>();
        var user = context.Users.First();
        var contract = context.Contracts.First();
        var sub = context.SubContracts.First();
        var plan = context.HealthPlans.First();

        var client = _factory.CreateClient();
        var cmd = new CreatePreRegistrationCommand(user.Id, contract.Id, sub.Id, plan.Id, "Employee");
        var response = await client.PostAsJsonAsync("/api/preregistrations", cmd);
        response.EnsureSuccessStatusCode();
    }
}

using InsuranceSuite.Infrastructure.Persistence;
using InsuranceSuite.Infrastructure.Seed;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceSuite.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<InsuranceDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<InsuranceDbContext>(options =>
                options.UseInMemoryDatabase("TestDb" + Guid.NewGuid()));

            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<InsuranceDbContext>();
            db.Database.EnsureCreated();
            SeedData.InitializeAsync(db).GetAwaiter().GetResult();
        });
    }
}

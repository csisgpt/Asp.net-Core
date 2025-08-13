using InsuranceSuite.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSuite.Infrastructure.Persistence.Configurations;

public class HealthPlanConfiguration : IEntityTypeConfiguration<HealthPlan>
{
    public void Configure(EntityTypeBuilder<HealthPlan> builder)
    {
        builder.ToTable("HealthPlans");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Title).HasMaxLength(200).IsRequired();
        builder.HasIndex(h => new { h.SubContractId, h.Title }).IsUnique();
        builder.HasOne<SubContract>()
            .WithMany(s => s.Plans)
            .HasForeignKey(h => h.SubContractId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

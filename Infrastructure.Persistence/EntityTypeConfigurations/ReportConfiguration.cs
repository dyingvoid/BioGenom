using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("reports");
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id");
        builder
            .Property(x => x.UserId)
            .HasColumnName("user_id");
        builder
            .HasIndex(x => x.UserId)
            .IsUnique();
        builder
            .HasMany(x => x.NutrientReports)
            .WithOne(x => x.Report)
            .HasForeignKey(x => x.ReportId);
    }
}
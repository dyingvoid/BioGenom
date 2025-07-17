using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class NutrientReportConfiguration : IEntityTypeConfiguration<NutrientReport>
{
    public void Configure(EntityTypeBuilder<NutrientReport> builder)
    {
        builder.ToTable("nutrient_reports");
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id");
        
        builder
            .Property(x => x.CurrentIntake)
            .HasColumnName("current_intake")
            .IsRequired();
        builder
            .Property(x => x.ReportId)
            .HasColumnName("report_id");
        builder
            .Property(x => x.NutrientId)
            .HasColumnName("nutrient_id");
        builder
            .HasOne(x => x.Nutrient)
            .WithMany()
            .HasForeignKey(x => x.NutrientId);
    }
}
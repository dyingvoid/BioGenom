using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class NutrientConfiguration : IEntityTypeConfiguration<Nutrient>
{
    public void Configure(EntityTypeBuilder<Nutrient> builder)
    {
        builder.ToTable("nutrients");
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id");
        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();
        builder
            .Property(x => x.MinAmount)
            .HasColumnName("min_amount");
        builder
            .Property(x => x.MaxAmount)
            .HasColumnName("max_amount");
        
        builder
            .HasIndex(x => x.Name)
            .IsUnique();

        builder
            .Property(x => x.UnitId)
            .HasColumnName("unit_id");
        builder
            .HasOne(x => x.Unit)
            .WithMany()
            .HasForeignKey(x => x.UnitId);
    }
}
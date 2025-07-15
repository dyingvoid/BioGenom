using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.EntityTypeConfigurations;

public class DrugNutrientConfiguration : IEntityTypeConfiguration<DrugNutrient>
{
    public void Configure(EntityTypeBuilder<DrugNutrient> builder)
    {
        builder.ToTable("drugs_nutrients");
        
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id");
        builder
            .Property(x => x.DrugId)
            .HasColumnName("drug_id");
        builder
            .Property(x => x.NutrientId)
            .HasColumnName("nutrient_id");
        builder
            .Property(x => x.Amount)
            .HasColumnName("amount");

        builder
            .HasOne(x => x.Drug)
            .WithMany(x => x.DrugNutrients)
            .HasForeignKey(x => x.DrugId);
        
        builder
            .HasOne(x => x.Nutrient)
            .WithMany()
            .HasForeignKey(x => x.NutrientId);;
    }
}
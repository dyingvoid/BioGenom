using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.EntityTypeConfigurations;

public class DrugConfiguration : IEntityTypeConfiguration<Drug>
{
    public void Configure(EntityTypeBuilder<Drug> builder)
    {
        builder.ToTable("drugs");
        
        builder.HasKey(x => x.Id);
        
        builder
            .Property(x => x.Id)
            .HasColumnName("id");
        builder
            .Property(x => x.Price)
            .HasColumnName("price")
            .IsRequired();
        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();
        builder
            .Property(x => x.Description)
            .HasColumnName("description")
            .IsRequired();
        
        builder
            .HasOne(x => x.Manufacturer)
            .WithMany()
            .HasForeignKey(x => x.ManufacturerId);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.EntityTypeConfigurations;

public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
{
    public void Configure(EntityTypeBuilder<Manufacturer> builder)
    {
        builder.ToTable("manufacturers");
        builder.HasKey(m => m.Id);
        builder
            .Property(m => m.Id)
            .HasColumnName("id");
        builder
            .Property(m => m.Name)
            .HasColumnName("name");
    }
}
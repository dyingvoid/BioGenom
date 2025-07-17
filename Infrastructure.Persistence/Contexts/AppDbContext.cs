using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Report> Reports { get; set; }
    public DbSet<Nutrient> Nutrients { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
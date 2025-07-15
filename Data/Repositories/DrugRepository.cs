using Business.Interfaces;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Repositories;

public class DrugRepository : IDrugRepository
{
    private readonly AppDbContext _context;

    public DrugRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Insert(Drug drug, CancellationToken ct = default)
    {
        var manufacturer = await _context.Manufacturers
            .FirstOrDefaultAsync(m => m.Id == drug.ManufacturerId, ct);
        if (manufacturer == null)
            throw new KeyNotFoundException($"Manufacturer with id: {drug.ManufacturerId} does not exist");

        _context.Drugs.Add(drug);
        await _context.SaveChangesAsync(ct);
    }

    public Task<List<Drug>> GetByNutrientId(int nutrientId, CancellationToken ct = default)
    {
        return _context.Drugs
            .Where(d => d.DrugNutrients.Any(dn => dn.NutrientId == nutrientId))
            .ToListAsync(ct);
    }
}
using Business.Interfaces;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Repositories;

public class NutrientRepository : INutrientRepository
{
    private readonly AppDbContext _context;

    public NutrientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Nutrient>> GetByNames(List<string> names, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(names);
        if (names.Count == 0)
            return [];

        var nutrients = await _context.Nutrients
            .Where(nutrient => names.Contains(nutrient.Name))
            .ToListAsync(ct);
        if (nutrients.Count == names.Count)
            return nutrients;

        var foundNames = new HashSet<string>(
            nutrients.Select(nutrient => nutrient.Name)
        );
        var missingNames = names
            .Where(name => !foundNames.Contains(name))
            .ToList();
        throw new KeyNotFoundException(
            $"The following nutrient names were not found: {string.Join(", ", missingNames)}"
        );
    }
}
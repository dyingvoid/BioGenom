using Models;

namespace Business.Interfaces;

public interface INutrientRepository
{
    Task<List<Nutrient>> GetByNames(List<string> names, CancellationToken ct = default);
}
using Models;

namespace Business.Interfaces;

public interface IDrugRepository
{
    Task Insert(Drug drug, CancellationToken ct = default);
    Task<List<Drug>> GetByNutrientId(int nutrientId, CancellationToken ct = default);
}
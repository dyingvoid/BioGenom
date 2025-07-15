using Business.Dtos;
using Business.Interfaces;
using Models;

namespace Business.Services;

public class DrugService
{
    private readonly IDrugRepository _drugRepository;
    private readonly INutrientRepository _nutrientRepository;

    public DrugService(
        IDrugRepository drugRepository,
        INutrientRepository nutrientRepository)
    {
        _drugRepository = drugRepository;
        _nutrientRepository = nutrientRepository;
    }

    public async Task<Guid> AddDrug(DrugDto drugDto, CancellationToken ct = default)
    {
        var nutrientNames = drugDto.Nutrients
            .Select(n => n.NutrientName)
            .ToList();
        var nutrients = await _nutrientRepository
            .GetByNames(nutrientNames, ct);

        var nutrientMap = drugDto.Nutrients
            .ToDictionary(n => n.NutrientName, n => n);
        var drugId = Guid.NewGuid();
        var drugNutrients = nutrients
            .Select(n => new DrugNutrient
                {
                    Id = Guid.NewGuid(),
                    DrugId = drugId,
                    NutrientId = n.Id,
                    Amount = nutrientMap[n.Name].Amount,
                }
            ).ToList();
        var drug = new Drug
        {
            Id = drugId,
            Price = drugDto.Price,
            Name = drugDto.Name,
            Description = drugDto.Description,
            DrugNutrients = drugNutrients,
            ManufacturerId = drugDto.ManufacturerId
        };

        await _drugRepository.Insert(drug, ct);
        return drug.Id;
    }
}
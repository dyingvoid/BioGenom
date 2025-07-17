using Business.Dtos;
using Business.Interfaces;
using Models;

namespace Business.Services;

public class ReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly CacheService _cacheService;
    private readonly IDrugRecommendationApi _drugRecommendationApi;

    public ReportService(
        IReportRepository reportRepository,
        CacheService cacheService,
        IDrugRecommendationApi drugRecommendationApi)
    {
        _reportRepository = reportRepository;
        _cacheService = cacheService;
        _drugRecommendationApi = drugRecommendationApi;
    }

    public async Task<Guid> CreateReport(
        ReportRequestDto req, CancellationToken ct = default)
    {
        var nutrientReports = req.NutrientReports
            .Select(nrp => new NutrientReport
                {
                    Id = Guid.NewGuid(),
                    ReportId = req.ReportId,
                    NutrientId = nrp.NutrientId,
                    CurrentIntake = nrp.CurrentIntake,
                }
            )
            .ToList();
        var report = new Report
        {
            Id = req.ReportId,
            UserId = req.UserId,
            NutrientReports = nutrientReports,
        };
        await _cacheService.RemoveAsync(CreateKey(req.UserId), ct);
        await _reportRepository.InsertFastReportAsync(report, ct);

        return report.Id;
    }

    public async Task<ReportResponseDto?> GetReportByUserId(Guid userId, CancellationToken ct = default)
    {
        var cacheKey = CreateKey(userId);
        var cached = await _cacheService.GetAsync<ReportResponseDto>(cacheKey, ct);
        if (cached != null)
        {
            return cached;
        }

        var nutrientReports = await GetNutrientReportsAsync(userId, ct);
        if (nutrientReports == null)
            return null;

        var recommendedDrugsRes = await _drugRecommendationApi
            .GetRecommendedDrugs(userId, nutrientReports, ct);

        var hs = nutrientReports
            .ToDictionary(nr => nr.NutrientId, nr => nr);
        var recommendedIntakes = recommendedDrugsRes.Drugs
            .SelectMany(d => d.Nutrients)
            .Select(drugNutrient =>
            {
                var ok = hs.TryGetValue(drugNutrient.NutrientId, out var nutrient);
                if (!ok)
                    return null;

                var recommendedIntake = new RecommendedNutrientIntakeDto(
                    nutrientId: nutrient!.NutrientId,
                    nutrientName: nutrient.NutrientName,
                    currentIntake: nutrient.CurrentIntake,
                    drugIntake: drugNutrient.Amount,
                    recommendedIntake: nutrient.MinIntake
                );

                return recommendedIntake;
            })
            .Where(intake => intake != null)
            .ToList();

        var res = new ReportResponseDto
        {
            NutrientReports = nutrientReports,
            RecommendedNutrientIntake = recommendedIntakes!,
        };
        _ = _cacheService.SetAsync(cacheKey, res, ct);

        return res;
    }

    private async Task<List<NutrientReportResponseDto>?> GetNutrientReportsAsync(Guid userId, CancellationToken ct)
    {
        var report = await _reportRepository.GetFastReportByUserIdAsync(userId, ct);

        var nutrientReports = report?.NutrientReports
            .Select(nrp => new NutrientReportResponseDto
                {
                    NutrientId = nrp.NutrientId,
                    NutrientName = nrp.Nutrient.Name,
                    CurrentIntake = nrp.CurrentIntake,
                    MinIntake = nrp.Nutrient.MinAmount,
                    MaxIntake = nrp.Nutrient.MaxAmount,
                }
            )
            .ToList();
        return nutrientReports;
    }

    private static string CreateKey(Guid userId)
    {
        return $"fast-report:{userId}";
    }
}
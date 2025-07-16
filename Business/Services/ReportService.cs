using System.Text.Json;
using Business.Dtos;
using Business.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Models;

namespace Business.Services;

public class ReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly IDistributedCache _cache;

    public ReportService(
        IReportRepository reportRepository,
        IDistributedCache cache)
    {
        _reportRepository = reportRepository;
        _cache = cache;
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
        await InvalidateCacheFastReport(req.UserId, ct);
        await _reportRepository.InsertFastReportAsync(report, ct);

        return report.Id;
    }

    public async Task<ReportResponseDto?> GetReportById(Guid userId, CancellationToken ct = default)
    {
        var cached = await GetFastReportFromCache(userId, ct);
        if (cached != null)
        {
            return cached;
        }

        var report = await _reportRepository.GetFastReportByUserIdAsync(userId, ct);
        if (report is null)
        {
            return null;
        }

        var nutrientReports = report.NutrientReports
            .Select(nrp => new NutrientReportResponseDto
                {
                    NutrientName = nrp.Nutrient.Name,
                    CurrentIntake = nrp.CurrentIntake,
                    MinIntake = nrp.Nutrient.MinAmount,
                    MaxIntake = nrp.Nutrient.MaxAmount,
                }
            )
            .ToList();
        var res = new ReportResponseDto
        {
            NutrientReports = nutrientReports,
        };

        return res;
    }

    private async Task<ReportResponseDto?> GetFastReportFromCache(Guid userId, CancellationToken ct)
    {
        var cacheKey = CreateKey(userId);
        var cached = await _cache.GetStringAsync(cacheKey, ct);
        return cached is null 
            ? null 
            : JsonSerializer.Deserialize<ReportResponseDto>(cached);
    }

    private Task InvalidateCacheFastReport(Guid userId, CancellationToken ct)
    {
        var cacheKey = CreateKey(userId);
        return _cache.RemoveAsync(cacheKey, ct);
    }
    
    private static string CreateKey(Guid userId)
    {
        return $"fast-report:{userId}";
    }
}
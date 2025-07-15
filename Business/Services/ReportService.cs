using Business.Dtos;
using Business.Interfaces;
using Models;

namespace Business.Services;

public class ReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
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
        await _reportRepository.Insert(report, ct);

        return report.Id;
    }

    public async Task<ReportResponseDto?> GetReportById(Guid id, CancellationToken ct = default)
    {
        var report = await _reportRepository.GetById(id, ct);
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
}
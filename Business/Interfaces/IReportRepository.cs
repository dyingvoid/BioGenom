using Models;

namespace Business.Interfaces;

public interface IReportRepository
{
    Task UpsertFastReport(Report report, CancellationToken ct = default);
    Task<Report?> GetFastReportByUserIdAsync(Guid userId, CancellationToken ct = default);
}
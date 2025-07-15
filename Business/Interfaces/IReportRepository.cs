using Models;

namespace Business.Interfaces;

public interface IReportRepository
{
    Task Insert(Report report, CancellationToken ct = default);
    Task<Report?> GetById(Guid id, CancellationToken ct = default);
}
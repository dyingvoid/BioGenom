using Business.Interfaces;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task Insert(Report report, CancellationToken ct = default)
    {
        _context.Reports.Add(report);
        return _context.SaveChangesAsync(ct);
    }

    public Task<Report?> GetById(Guid id, CancellationToken ct = default)
    {
        return _context.Reports
            .Include(x => x.NutrientReports)
            .ThenInclude(x => x.Nutrient)
            .ThenInclude(x => x.Unit)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }
}
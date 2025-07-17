using Business.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure.Persistence.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task UpsertFastReport(Report report, CancellationToken ct = default)
    {
        var old = await _context.Reports
            .Where(r => r.UserId == report.UserId)
            .ToListAsync(ct);
        if (old.Count != 0)
        {
            _context.Reports.RemoveRange(old);
        }
        
        await _context.Reports.AddAsync(report, ct);
        await _context.SaveChangesAsync(ct);
    }

    public Task<Report?> GetFastReportByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return _context.Reports
            .Include(x => x.NutrientReports)
            .ThenInclude(x => x.Nutrient)
            .ThenInclude(x => x.Unit)
            .FirstOrDefaultAsync(x => x.UserId == userId, ct);
    }
}
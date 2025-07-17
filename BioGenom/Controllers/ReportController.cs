using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BioGenom.Controllers;

[ApiController]
[Route("/reports")]
public class ReportController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost("fast")]
    public async Task<IActionResult> CreateReport(
        [FromBody] ReportRequestDto dto, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _reportService.CreateReport(dto, ct);
        return Created($"/report/{id}", id);
    }

    [HttpGet("fast/{userId}")]
    public async Task<IActionResult> GetReport(Guid userId, CancellationToken ct = default)
    {
        var report = await _reportService.GetReportByUserId(userId, ct);
        if (report == null)
        {
            return NotFound();
        }

        return Ok(report);
    }
}
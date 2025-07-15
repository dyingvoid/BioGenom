using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BioGenom.Controllers;

[ApiController]
[Route("report")]
public class ReportController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateReport(
        [FromBody] ReportRequestDto dto, CancellationToken ct = default)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var id = await _reportService.CreateReport(dto, ct);
        return Created($"/report/{id}", id);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetReport(Guid id, CancellationToken ct = default)
    {
        var report = await _reportService.GetReportById(id, ct);
        if (report == null)
        {
            return NotFound();
        }
        return Ok(report);
    }
}
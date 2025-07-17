using Microsoft.AspNetCore.Mvc;

namespace BioGenom.Controllers;

[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpGet("/healthcheck")]
    public IActionResult Get(CancellationToken ct = default)
    {
        return Ok("Healthy");
    }
}
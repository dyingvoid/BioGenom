using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BioGenom.Controllers;

[ApiController]
[Route("drug")]
public class DrugController : ControllerBase
{
    private readonly DrugService _drugService;

    public DrugController(DrugService drugService)
    {
        _drugService = drugService;
    }

    [HttpPost]
    public async Task<IActionResult> AddDrug([FromBody] DrugDto drugDto, CancellationToken ct = default)
    {
        var id = await _drugService.AddDrug(drugDto, ct);
        return Created($"/drug/{id}", id);
    }
}
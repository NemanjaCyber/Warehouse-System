using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Application.DTOs;
using WarehouseSystem.Application.Interfaces;

namespace WarehouseSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZaliheController(IStanjeZalihaService zaliheService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await zaliheService.GetAllAsync());

    [HttpPost("povecaj")]
    public async Task<IActionResult> Povecaj(AzurirajZaliheDto dto)
    {
        await zaliheService.PovecajZaliheAsync(dto);
        return Ok("Zalihe su ažurirane.");
    }

    [HttpPost("smanji")]
public async Task<IActionResult> Smanji(AzurirajZaliheDto dto)
{
    try 
    {
        await zaliheService.SmanjiZaliheAsync(dto);
        return Ok("Zalihe su umanjene.");
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(ex.Message);
    }
}
}
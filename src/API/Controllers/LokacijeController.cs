using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Application.DTOs;
using WarehouseSystem.Application.Interfaces;

namespace WarehouseSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LokacijeController(ILokacijaService lokacijaService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await lokacijaService.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(CreateLokacijaDto dto)
    {
        var id = await lokacijaService.CreateAsync(dto);
        return Ok(id);
    }
}
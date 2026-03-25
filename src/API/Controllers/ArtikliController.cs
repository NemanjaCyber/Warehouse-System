using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Application.DTOs;
using WarehouseSystem.Application.Interfaces;

namespace WarehouseSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtikliController(IArtikalService artikalService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArtikalDto>>> GetAll()
    {
        var artikli = await artikalService.GetAllAsync();
        return Ok(artikli);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArtikalDto>> GetById(int id)
    {
        var artikal = await artikalService.GetByIdAsync(id);
        if (artikal == null) return NotFound();
        return Ok(artikal);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateArtikalDto dto)
    {
        var id = await artikalService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await artikalService.DeleteAsync(id);
        return NoContent();
    }
}
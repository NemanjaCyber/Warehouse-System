using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Application.DTOs;
using WarehouseSystem.Application.Interfaces;
using WarehouseSystem.Domain.Entities;

namespace WarehouseSystem.Application.Services;

public class LokacijaService(IApplicationDbContext context) : ILokacijaService
{
    public async Task<IEnumerable<LokacijaDto>> GetAllAsync()
    {
        return await context.Lokacije
            .Select(l => new LokacijaDto(l.Id, l.Naziv, l.Kod, l.Opis))
            .ToListAsync();
    }

    public async Task<int> CreateAsync(CreateLokacijaDto dto)
    {
        var lokacija = new Lokacija { Naziv = dto.Naziv, Kod = dto.Kod, Opis = dto.Opis };
        context.Lokacije.Add(lokacija);
        await context.SaveChangesAsync();
        return lokacija.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var lokacija = await context.Lokacije.FindAsync(id);
        if (lokacija != null)
        {
            context.Lokacije.Remove(lokacija);
            await context.SaveChangesAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Application.DTOs;
using WarehouseSystem.Application.Interfaces;
using WarehouseSystem.Domain.Entities;

namespace WarehouseSystem.Application.Services;

public class ArtikalService(IApplicationDbContext context) : IArtikalService
{
    public async Task<IEnumerable<ArtikalDto>> GetAllAsync()
    {
        return await context.Artikli
            .Select(a => new ArtikalDto(
                a.Id, 
                a.Naziv, 
                a.Sifra, 
                a.Opis, 
                a.Cena, 
                a.MinimalnaKolicina))
            .ToListAsync();
    }

    public async Task<ArtikalDto?> GetByIdAsync(int id)
    {
        var a = await context.Artikli.FindAsync(id);
        
        if (a == null) return null;

        return new ArtikalDto(
            a.Id, 
            a.Naziv, 
            a.Sifra, 
            a.Opis, 
            a.Cena, 
            a.MinimalnaKolicina);
    }

    public async Task<int> CreateAsync(CreateArtikalDto dto)
    {
        var artikal = new Artikal 
        { 
            Naziv = dto.Naziv, 
            Sifra = dto.Sifra, 
            Opis = dto.Opis,
            Cena = dto.Cena,
            MinimalnaKolicina = dto.MinimalnaKolicina
        };

        context.Artikli.Add(artikal);
        await context.SaveChangesAsync();
        return artikal.Id;
    }

    public async Task UpdateAsync(int id, CreateArtikalDto dto)
    {
        var artikal = await context.Artikli.FindAsync(id);
        
        if (artikal != null)
        {
            artikal.Naziv = dto.Naziv;
            artikal.Sifra = dto.Sifra;
            artikal.Opis = dto.Opis;
            artikal.Cena = dto.Cena;
            artikal.MinimalnaKolicina = dto.MinimalnaKolicina;
            
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var artikal = await context.Artikli.FindAsync(id);
        
        if (artikal != null)
        {
            context.Artikli.Remove(artikal);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<TransakcijaZalihaDto>> GetIstorijaTransakcijaAsync(int artikalId)
    {
        return await context.TransakcijeZaliha
            .Include(t => t.Artikal)
            .Where(t => t.ArtikalId == artikalId)
            .OrderByDescending(t => t.DatumVreme)
            .Select(t => new TransakcijaZalihaDto
            {
                Id = t.Id,
                ArtikalId = t.ArtikalId,
                ArtikalNaziv = t.Artikal.Naziv,
                Kolicina = t.Kolicina,
                Tip = t.Tip.ToString(), // Enum u string
                DatumVreme = t.DatumVreme,
                KorisnikId = t.KorisnikId,
                Napomena = t.Napomena
            })
            .ToListAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Application.DTOs;
using WarehouseSystem.Application.Interfaces;
using WarehouseSystem.Domain.Entities;
using WarehouseSystem.Domain.Enums;

namespace WarehouseSystem.Application.Services;

public class StanjeZalihaService(IApplicationDbContext context, IKafkaProducer kafkaProducer) : IStanjeZalihaService
{
    public async Task<IEnumerable<StanjeZalihaDto>> GetAllAsync()
    {
        return await context.StanjeZaliha
            .Include(z=>z.Artikal)
            .Include(z=>z.Lokacija)
            .Select(z=>new StanjeZalihaDto(
                z.Id,
                z.Artikal.Naziv,
                z.Lokacija.Kod,
                z.Kolicina,
                z.PoslednjeAzuriranje
            )).ToListAsync();
    }

    public async Task PovecajZaliheAsync(AzurirajZaliheDto dto)
    {
        var stanje=await context.StanjeZaliha //trazimo da li postoji taj artikal na trazenoj lokaciji
            .FirstOrDefaultAsync(z=>z.ArtikalId==dto.ArtikalId && z.LokacijaId==dto.LokacijaId);

        if (stanje==null)//ako ne postoji, kreiramo ga
        {
            stanje=new StanjeZaliha
            {
                ArtikalId=dto.ArtikalId,
                LokacijaId=dto.LokacijaId,
                Kolicina=dto.Kolicina,
                PoslednjeAzuriranje=DateTime.UtcNow
            };
            context.StanjeZaliha.Add(stanje);
        }
        else
        {
            //ako postoji, samo dodajemo kolicinu
            stanje.Kolicina += dto.Kolicina;
            stanje.PoslednjeAzuriranje = DateTime.UtcNow;
        }

        // 2. KREIRAMO AUDIT LOG (Tip: Ulaz)
        var log = new TransakcijaZaliha
        {
            ArtikalId = dto.ArtikalId,
            Kolicina = dto.Kolicina,
            Tip = TipTransakcije.Ulaz, // Koristimo tvoj Enum
            DatumVreme = DateTime.UtcNow,
            KorisnikId = "Sistem_Dev",
            Napomena = $"Prijem robe na lokaciju {dto.LokacijaId}"
        };

        context.TransakcijeZaliha.Add(log);

        await context.SaveChangesAsync();
    }

    public async Task SmanjiZaliheAsync(AzurirajZaliheDto dto)
    {
        var stanje = await context.StanjeZaliha
            .Include(z => z.Artikal) //Ucitavamo artikal da bismo videli MinimalnaKolicina
            .FirstOrDefaultAsync(z => z.ArtikalId == dto.ArtikalId && z.LokacijaId == dto.LokacijaId);
        
        if (stanje == null) throw new InvalidOperationException("Zaliha ne postoji.");

        // VALIDACIJA: Provera pre smanjenja
        if (stanje.Kolicina < dto.Kolicina)
        {
            throw new InvalidOperationException(
                $"Nedovoljno zaliha! Pokušano smanjenje za {dto.Kolicina}, a trenutno stanje je {stanje.Kolicina}.");
        }

        stanje.Kolicina -= dto.Kolicina;
        stanje.PoslednjeAzuriranje = DateTime.UtcNow;

        var log = new TransakcijaZaliha
        {
            ArtikalId = dto.ArtikalId,
            Kolicina = dto.Kolicina, // Čuvamo koliko je skinuto
            Tip = TipTransakcije.Izlaz,
            DatumVreme = DateTime.UtcNow,
            KorisnikId = "Sistem_Dev", // Kasnije ovde ide ID ulogovanog korisnika
            Napomena = $"Automatsko smanjenje zaliha na lokaciji {dto.LokacijaId}"
        };

        context.TransakcijeZaliha.Add(log);

        // 3. Kafka Alert (ostaje isto)
        if (stanje.Kolicina < stanje.Artikal.MinimalnaKolicina)
        {
            await kafkaProducer.SendAlertAsync("stock-low-alerts", new { ArtikalId = stanje.ArtikalId, Trenutno = stanje.Kolicina });
        }

        await context.SaveChangesAsync();
    }
}
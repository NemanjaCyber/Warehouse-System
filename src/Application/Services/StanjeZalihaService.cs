using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Application.DTOs;
using WarehouseSystem.Application.Interfaces;
using WarehouseSystem.Domain.Entities;

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

        await context.SaveChangesAsync();
    }

    public async Task SmanjiZaliheAsync(AzurirajZaliheDto dto)
    {
        var stanje = await context.StanjeZaliha
            .Include(z => z.Artikal) //Ucitavamo artikal da bismo videli MinimalnaKolicina
            .FirstOrDefaultAsync(z => z.ArtikalId == dto.ArtikalId && z.LokacijaId == dto.LokacijaId);
        
        if (stanje == null || stanje.Kolicina < dto.Kolicina)//ako hocemo da smanjimo vise nego sto zapravo ima u magacinu
        {
            throw new InvalidOperationException("Nedovoljno zaliha na lokaciji.");
        }

        stanje.Kolicina -= dto.Kolicina;
        stanje.PoslednjeAzuriranje = DateTime.UtcNow;

        if(stanje.Kolicina<stanje.Artikal.MinimalnaKolicina)
        {
            var alert=new
            {
                ArtikalId = stanje.ArtikalId, 
                Naziv = stanje.Artikal.Naziv, 
                Trenutno = stanje.Kolicina, 
                Minimum = stanje.Artikal.MinimalnaKolicina,
                Poruka = "Zalihe su ispod minimuma!"
            };

            Console.WriteLine($"[DEBUG] Prag dostignut! Šaljem poruku na Kafku za {stanje.Artikal.Naziv}...");
            await kafkaProducer.SendAlertAsync("stock-low-alerts", alert);//ako je trenutna kolicina manja od minimalne dozvoljene,
                                                                    //saljemo alert poruku preko kafka pruducera
        }

        await context.SaveChangesAsync();
    }
}
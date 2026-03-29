using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Domain.Entities;

namespace WarehouseSystem.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Artikal> Artikli { get; }
    DbSet<Lokacija> Lokacije { get; }
    DbSet<StanjeZaliha> StanjeZaliha { get; }
    DbSet<TransakcijaZaliha> TransakcijeZaliha { get; } // Dodaj ovo

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
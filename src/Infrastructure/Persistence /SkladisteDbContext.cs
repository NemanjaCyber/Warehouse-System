using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Domain.Entities;

namespace WarehouseSystem.Infrastructure.Persistence;

public class SkladisteDbContext : DbContext
{
    public SkladisteDbContext(DbContextOptions<SkladisteDbContext> options) : base(options) { }

    // Naše tabele (DbSet-ovi)
    public DbSet<Artikal> Artikli => Set<Artikal>();
    public DbSet<Lokacija> Lokacije => Set<Lokacija>();
    public DbSet<StanjeZaliha> StanjaZaliha => Set<StanjeZaliha>();
    public DbSet<TransakcijaZaliha> TransakcijeZaliha => Set<TransakcijaZaliha>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ovde možemo definisati specifična pravila (npr. Sifra mora biti unikatna)
        modelBuilder.Entity<Artikal>()
            .HasIndex(a => a.Sifra)
            .IsUnique();

        // Konfiguracija decimalne vrednosti za cenu
        modelBuilder.Entity<Artikal>()
            .Property(a => a.Cena)
            .HasPrecision(18, 2);
            
        // Definisanje veze 1-n ili n-n ako EF sam ne prepozna (mada će ovde prepoznati)
    }
}
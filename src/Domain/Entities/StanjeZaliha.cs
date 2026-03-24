namespace WarehouseSystem.Domain.Entities;

public class StanjeZaliha
{
    public int Id { get; set; }
    public int ArtikalId { get; set; }
    public Artikal Artikal { get; set; } = null!;

    public int LokacijaId { get; set; }
    public Lokacija Lokacija { get; set; } = null!;

    public int TrenutnaKolicina { get; set; }
    public DateTime PoslednjeAzuriranje { get; set; }
}
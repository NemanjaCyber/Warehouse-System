namespace WarehouseSystem.Domain.Entities;

public class Artikal
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public string Sifra { get; set; } = string.Empty; // SKU (Stock Keeping Unit)
    public string Opis { get; set; } = string.Empty;
    public decimal Cena { get; set; }
    public int MinimalnaKolicina { get; set; } // Za Kafka alert (StockLow)

    public List<StanjeZaliha> Zalihe { get; set; } = new();
}
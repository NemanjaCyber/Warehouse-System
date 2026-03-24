using WarehouseSystem.Domain.Enums;

namespace WarehouseSystem.Domain.Entities;

public class TransakcijaZaliha
{
    public int Id { get; set; }
    public int ArtikalId { get; set; }
    public Artikal Artikal { get; set; } = null!;
    
    public int Kolicina { get; set; }
    public TipTransakcije Tip { get; set; }
    public DateTime DatumVreme { get; set; }
    public string KorisnikId { get; set; } = string.Empty; // Ko je izvršio promenu
    public string Napomena { get; set; } = string.Empty;
}
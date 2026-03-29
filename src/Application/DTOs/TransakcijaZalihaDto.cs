namespace WarehouseSystem.Application.DTOs;

public class TransakcijaZalihaDto
{
    public int Id { get; set; }
    public int ArtikalId { get; set; }
    public string ArtikalNaziv { get; set; } = string.Empty;
    public int Kolicina { get; set; }
    public string Tip { get; set; } = string.Empty;
    public DateTime DatumVreme { get; set; }
    public string KorisnikId { get; set; } = string.Empty;
    public string Napomena { get; set; } = string.Empty;
}
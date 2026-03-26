namespace WarehouseSystem.Domain.Entities;

public class Lokacija
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty; // npr. "Sektor Sever"
    public string Kod { get; set; } = string.Empty;   // npr. "SEC-A-01"
    public string Opis { get; set; } = string.Empty;

    // Relacija: Jedna lokacija može imati mnogo zapisa o stanju zaliha
    public List<StanjeZaliha> Zalihe { get; set; } = new();
}
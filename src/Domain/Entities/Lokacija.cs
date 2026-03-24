namespace WarehouseSystem.Domain.Entities;

public class Lokacija
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty; // Npr. "Sektor A - Polica 4"
    public string KodLokacije { get; set; } = string.Empty;

    // Relacije
    public List<StanjeZaliha> Zalihe { get; set; } = new();
}
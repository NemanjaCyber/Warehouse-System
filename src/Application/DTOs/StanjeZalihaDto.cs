namespace WarehouseSystem.Application.DTOs;

public record StanjeZalihaDto(
    int Id, 
    string ArtikalNaziv, 
    string LokacijaKod, 
    int Kolicina, 
    DateTime PoslednjaIzmena
);

public record AzurirajZaliheDto(
    int ArtikalId, 
    int LokacijaId, 
    int Kolicina
);
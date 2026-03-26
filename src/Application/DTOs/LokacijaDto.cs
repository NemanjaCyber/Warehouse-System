namespace WarehouseSystem.Application.DTOs;

public record LokacijaDto(
    int Id, 
    string Naziv, 
    string Kod, 
    string Opis
);

public record CreateLokacijaDto(
    string Naziv, 
    string Kod, 
    string Opis
);
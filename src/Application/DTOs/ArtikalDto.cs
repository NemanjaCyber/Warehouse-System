namespace WarehouseSystem.Application.DTOs;

// Ono što šaljemo klijentu (npr. u listi svih artikala)
public record ArtikalDto(
    int Id, 
    string Naziv, 
    string Sifra, 
    string Opis, 
    decimal Cena, 
    int MinimalnaKolicina
);

// Ono što primamo od klijenta kada se kreira novi artikal
public record CreateArtikalDto(
    string Naziv, 
    string Sifra, 
    string Opis, 
    decimal Cena, 
    int MinimalnaKolicina
);
using WarehouseSystem.Application.DTOs;

namespace WarehouseSystem.Application.Interfaces;

public interface IArtikalService
{
    // Vraćamo listu DTO-ova klijentu
    Task<IEnumerable<ArtikalDto>> GetAllAsync();
    
    // Vraćamo jedan DTO (ili null ako ne postoji)
    Task<ArtikalDto?> GetByIdAsync(int id);
    
    // Primamo DTO za kreiranje, vraćamo ID novog artikla
    Task<int> CreateAsync(CreateArtikalDto dto);
    
    // Primamo ID i DTO sa novim podacima za izmenu
    Task UpdateAsync(int id, CreateArtikalDto dto);
    
    // Brišemo po ID-u
    Task DeleteAsync(int id);
}
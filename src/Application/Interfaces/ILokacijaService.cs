using WarehouseSystem.Application.DTOs;

namespace WarehouseSystem.Application.Interfaces;

public interface ILokacijaService
{
    Task<IEnumerable<LokacijaDto>> GetAllAsync();
    Task<int> CreateAsync(CreateLokacijaDto dto);
    Task DeleteAsync(int id);
}
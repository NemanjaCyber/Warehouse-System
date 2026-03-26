using WarehouseSystem.Application.DTOs;

namespace WarehouseSystem.Application.Interfaces;

public interface IStanjeZalihaService
{
    Task<IEnumerable<StanjeZalihaDto>> GetAllAsync();
    Task PovecajZaliheAsync(AzurirajZaliheDto dto);
    Task SmanjiZaliheAsync(AzurirajZaliheDto dto);
}
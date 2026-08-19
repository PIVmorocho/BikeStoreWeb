using BikeStore.API.Models.DTOs;

namespace BikeStore.API.Services
{
    public interface IClienteService
    {
        Task<IReadOnlyList<ClienteDto>> GetAllAsync();
        Task<ClienteDto> GetByIdAsync(int id);
        Task<ClienteDto> CreateAsync(ClienteCreateDto dto);
        Task<ClienteDto> UpdateAsync(int id, ClienteUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

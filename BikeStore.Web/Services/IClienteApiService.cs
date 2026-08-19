using BikeStore.Web.Models.Api;

namespace BikeStore.Web.Services
{
    public interface IClienteApiService
    {
        Task<List<ClienteDto>> GetAllAsync();
        Task<ClienteDto> GetByIdAsync(int id);
        Task CreateAsync(ClienteCreateDto dto);
        Task UpdateAsync(int id, ClienteUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

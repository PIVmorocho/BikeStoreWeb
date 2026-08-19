using BikeStore.Web.Models.Api;

namespace BikeStore.Web.Services
{
    public interface ICategoriaApiService
    {
        Task<List<CategoriaDto>> GetAllAsync();
        Task<CategoriaDto> GetByIdAsync(int id);
        Task CreateAsync(CategoriaCreateDto dto);
        Task UpdateAsync(int id, CategoriaUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

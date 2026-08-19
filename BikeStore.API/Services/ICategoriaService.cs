using BikeStore.API.Models.DTOs;

namespace BikeStore.API.Services
{
    public interface ICategoriaService
    {
        Task<IReadOnlyList<CategoriaDto>> GetAllAsync();
        Task<CategoriaDto> GetByIdAsync(int id);
        Task<CategoriaDto> CreateAsync(CategoriaCreateDto dto);
        Task<CategoriaDto> UpdateAsync(int id, CategoriaUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

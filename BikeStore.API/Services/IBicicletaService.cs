using BikeStore.API.Data.Repositories;
using BikeStore.API.Models.DTOs;

namespace BikeStore.API.Services
{
    public interface IBicicletaService
    {
        Task<IReadOnlyList<BicicletaDto>> BuscarAsync(BicicletaFiltro filtro);
        Task<BicicletaDto> GetByIdAsync(int id);
        Task<BicicletaDto> CreateAsync(BicicletaCreateDto dto);
        Task<BicicletaDto> UpdateAsync(int id, BicicletaUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

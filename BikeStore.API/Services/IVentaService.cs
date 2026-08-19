using BikeStore.API.Models.DTOs;

namespace BikeStore.API.Services
{
    public interface IVentaService
    {
        Task<VentaResponseDto> CrearVentaAsync(VentaCreateDto dto);
        Task<IReadOnlyList<VentaResponseDto>> GetAllAsync();
        Task<VentaResponseDto> GetByIdAsync(int id);
        Task<IReadOnlyList<VentaResponseDto>> GetByClienteAsync(int idCliente);
    }
}

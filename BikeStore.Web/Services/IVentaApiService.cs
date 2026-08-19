using BikeStore.Web.Models.Api;

namespace BikeStore.Web.Services
{
    public interface IVentaApiService
    {
        Task<List<VentaResponseDto>> GetAllAsync();
        Task<VentaResponseDto> GetByIdAsync(int id);
        Task<List<VentaResponseDto>> GetByClienteAsync(int idCliente);
        Task<VentaResponseDto> CrearAsync(VentaCreateDto dto);
    }
}

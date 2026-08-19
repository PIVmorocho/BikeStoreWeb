using BikeStore.Web.Models.Api;

namespace BikeStore.Web.Services
{
    public class BicicletaFiltro
    {
        public string? Nombre { get; set; }
        public int? Categoria { get; set; }
        public string? Marca { get; set; }
        public bool? StockBajo { get; set; }
        public bool? Agotado { get; set; }
    }

    public interface IBicicletaApiService
    {
        Task<List<BicicletaDto>> BuscarAsync(BicicletaFiltro filtro);
        Task<BicicletaDto> GetByIdAsync(int id);
        Task CreateAsync(BicicletaCreateDto dto);
        Task UpdateAsync(int id, BicicletaUpdateDto dto);
        Task DeleteAsync(int id);
    }
}

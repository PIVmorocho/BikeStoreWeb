using BikeStore.API.Models.Entities;

namespace BikeStore.API.Data.Repositories
{
    public class BicicletaFiltro
    {
        public string? Nombre { get; set; }
        public int? IdCategoria { get; set; }
        public string? Marca { get; set; }
        public bool? StockBajo { get; set; }
        public bool? Agotado { get; set; }
    }

    public interface IBicicletaRepository : IGenericRepository<Bicicleta>
    {
        Task<IReadOnlyList<Bicicleta>> BuscarAsync(BicicletaFiltro filtro, int umbralStockBajo);
        Task<Bicicleta?> GetByIdConCategoriaAsync(int id);
    }
}

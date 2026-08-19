using BikeStore.API.Models.Entities;

namespace BikeStore.API.Data.Repositories
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<IReadOnlyList<Venta>> GetAllConDetallesAsync();
        Task<Venta?> GetByIdConDetallesAsync(int id);
        Task<IReadOnlyList<Venta>> GetByClienteAsync(int idCliente);
    }
}

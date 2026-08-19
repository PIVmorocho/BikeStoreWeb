using BikeStore.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.API.Data.Repositories
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        public VentaRepository(BikeStoreDbContext context) : base(context)
        {
        }

        private IQueryable<Venta> QueryConDetalles() =>
            DbSet
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Bicicleta);

        public async Task<IReadOnlyList<Venta>> GetAllConDetallesAsync() =>
            await QueryConDetalles().OrderByDescending(v => v.Fecha).ToListAsync();

        public async Task<Venta?> GetByIdConDetallesAsync(int id) =>
            await QueryConDetalles().FirstOrDefaultAsync(v => v.IdVenta == id);

        public async Task<IReadOnlyList<Venta>> GetByClienteAsync(int idCliente) =>
            await QueryConDetalles()
                .Where(v => v.IdCliente == idCliente)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();
    }
}

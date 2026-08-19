using BikeStore.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.API.Data.Repositories
{
    public class BicicletaRepository : GenericRepository<Bicicleta>, IBicicletaRepository
    {
        public BicicletaRepository(BikeStoreDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Bicicleta>> BuscarAsync(BicicletaFiltro filtro, int umbralStockBajo)
        {
            var query = DbSet.Include(b => b.Categoria).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
                query = query.Where(b => b.Modelo.Contains(filtro.Nombre));

            if (filtro.IdCategoria.HasValue)
                query = query.Where(b => b.IdCategoria == filtro.IdCategoria.Value);

            if (!string.IsNullOrWhiteSpace(filtro.Marca))
                query = query.Where(b => b.Marca.Contains(filtro.Marca));

            if (filtro.Agotado == true)
                query = query.Where(b => b.Stock == 0);
            else if (filtro.StockBajo == true)
                query = query.Where(b => b.Stock > 0 && b.Stock <= umbralStockBajo);

            return await query.ToListAsync();
        }

        public async Task<Bicicleta?> GetByIdConCategoriaAsync(int id) =>
            await DbSet.Include(b => b.Categoria).FirstOrDefaultAsync(b => b.IdBicicleta == id);
    }
}

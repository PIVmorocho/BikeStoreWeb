using BikeStore.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.API.Data.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(BikeStoreDbContext context) : base(context)
        {
        }

        public async Task<bool> TieneBicicletasAsociadasAsync(int idCategoria) =>
            await Context.Bicicletas.AnyAsync(b => b.IdCategoria == idCategoria);
    }
}

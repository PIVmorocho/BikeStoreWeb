using BikeStore.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.API.Data.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(BikeStoreDbContext context) : base(context)
        {
        }

        public async Task<Cliente?> GetByCedulaAsync(string cedula) =>
            await DbSet.FirstOrDefaultAsync(c => c.Cedula == cedula);
    }
}
